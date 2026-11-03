using System;
using Unity.Cinemachine;
using UnityEngine;

public class GameState : MonoBehaviour
{
    # region Singleton
    private static GameState instance;
    public static GameState Instance { get => instance; }

    private void SingletonSetup()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }
    #endregion

    private CinemachineCamera cinemachineCamera;


    private PlayerSpawnpoint playerSpawnpoint;
    public PlayerSpawnpoint PlayerSpawnpoint { get => playerSpawnpoint; }
    public Transform PlayerSpawnAnchor { get => playerSpawnpoint.transform; }


    [Header("Readonly")]
    [Tooltip("Readonly. Technically you can alter but dont expect not to break things >:(")]

    [SerializeField] private int iterationCount;
    public int IterationCount { get => iterationCount; set => iterationCount = value; }

    [SerializeField] private Player playerReference;
    public Player PlayerReference
    {
        get { return playerReference; }
        set
        {
            playerReference = value;
            cinemachineCamera.Target.TrackingTarget = playerReference.transform;
            onPlayerReferenceChange.Invoke();
        }
    }

    public Action onPlayerReferenceChange;

    [SerializeField] private Camera playerCamera;
    public Camera PlayerCamera { get => playerCamera; set => playerCamera = value; }


    private void Awake()
    {
        SingletonSetup();
        FetchDependencies();
    }

    private void FetchDependencies()
    {
        cinemachineCamera = FindAnyObjectByType<CinemachineCamera>();
        playerSpawnpoint = FindAnyObjectByType<PlayerSpawnpoint>();
    }

}
