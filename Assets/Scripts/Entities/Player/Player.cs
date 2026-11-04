using System;
using Unity.Cinemachine;
using UnityEngine;

[RequireComponent(typeof(PlayerHorizontalMovement), typeof(PlayerCameraHandler), typeof(PlayerJump))]
[RequireComponent(typeof(PlayerSound))]
public class Player : Entity, IProduct, ISpikeAble
{
    public string ProductName { get => name; set => gameObject.name = value; }

    #region dependencies
    private Rigidbody rigidbody;
    private PlayerHorizontalMovement playerMovement;
    private VerticalState verticalState;
    private PlayerCameraHandler playerCameraHandler;
    private PlayerJump playerJump;
    private PlayerSound playerSound;
    #endregion

    public Action onDeath;

    private void Start()
    {
        Initialize();
    }

    private void FetchDependencies()
    {
        rigidbody = GetComponent<Rigidbody>();
        playerMovement = GetComponent<PlayerHorizontalMovement>();
        verticalState = GetComponent<VerticalState>();
        playerCameraHandler = GetComponent<PlayerCameraHandler>();
        playerJump = GetComponent<PlayerJump>();
        playerSound = GetComponent<PlayerSound>();
    }

    public void Initialize()
    {
        transform.position = GameState.Instance.PlayerSpawnAnchor.position;
        transform.rotation = GameState.Instance.PlayerSpawnAnchor.rotation;

        DebugVisualiser.Instance.FetchMeshRendererReferences();
    }

    public override void Kill()
    {
        if (isDead)
        {
            return;
        }
        base.Kill();
        onDeath();
        RemoveSoul();
        Respawn();
    }

    private void Respawn()
    {
        PlayerCorpseFactory.Instance.SpawnProduct(GameState.Instance.PlayerSpawnAnchor.position);
    }

    private void RemoveSoul()
    {
        tag = "Untagged";

        FetchDependencies();

        Destroy(this);

        TryRemoveComponent(playerJump);
        TryRemoveComponent(rigidbody);

        Destroy(verticalState.groundDetectorsParent);
        TryRemoveComponent(verticalState);

        TryRemoveComponent(playerMovement);
        TryRemoveComponent(playerCameraHandler);

        TryRemoveComponent(playerSound);
        TryRemoveComponent(playerSound.AudioSource);

    }

    /// <returns>Whether it was successful</returns>
    private bool TryRemoveComponent(Component component)
    {
        if (component == null)
        {
            QDebugManager.Instance.Important(this, "failed to destroy " + component.name + ".");
            return false;
        }
        Destroy(component);
        return true;
    }

}
