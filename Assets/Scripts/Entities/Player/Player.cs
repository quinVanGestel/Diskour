using Unity.Cinemachine;
using UnityEngine;

[RequireComponent(typeof(PlayerHorizontalMovement), typeof(PlayerCameraHandler), typeof(PlayerJump))]
public class Player : Entity, IProduct, ISpikeAble
{
    public string ProductName { get => name; set => gameObject.name = value; }

    private void Start()
    {
        Initialize();
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

        Destroy(this);

        Rigidbody rigidbody = GetComponent<Rigidbody>();
        if (rigidbody != null)
        {
            Destroy(rigidbody);
        }
        else
        {
            QDebugManager.Instance.Important(this, "failed to destroy rigidbody.");
        }

        PlayerHorizontalMovement playerMovement = GetComponent<PlayerHorizontalMovement>();
        if (playerMovement != null)
        {
            Destroy(playerMovement);
        }
        else
        {
            QDebugManager.Instance.Important(this, "failed to destroy playerMovement.");
        }

        VerticalState verticalState = GetComponent<VerticalState>();
        if (verticalState != null)
        {
            Destroy(verticalState.groundDetectorsParent);
            Destroy(verticalState);
        }
        else
        {
            QDebugManager.Instance.Important(this, "failed to destroy verticalState.");
        }
    }

}
