using Unity.Cinemachine;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement), typeof(Rigidbody))]

public class Player : Entity, IProduct
{
    public string ProductName { get => name; set => gameObject.name = value; }

    private bool isDead;

    private void Start()
    {
        Initialize();
    }

    public void Initialize()
    {
        transform.position = spawnAnchor.position;
        transform.rotation = spawnAnchor.rotation;

        CinemachineCamera cinemachineCamera = FindAnyObjectByType<CinemachineCamera>();
        cinemachineCamera.Target.TrackingTarget = transform;
        DebugVisualiser.Instance.RefreshMeshRendererReferences();
    }

    public override void Kill()
    {
        if (isDead)
        {
            return;
        }

        isDead = true;

        RemoveSoul();
        Respawn();
    }

    private void Respawn()
    {
        PlayerCorpseFactory.Instance.SpawnProduct(spawnAnchor.position);
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

        PlayerMovement playerMovement = GetComponent<PlayerMovement>();
        if (playerMovement != null)
        {
            Destroy(playerMovement);
        }
        else
        {
            QDebugManager.Instance.Important(this, "failed to destroy playerMovement.");
        }
    }

}
