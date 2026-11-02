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

    [SerializeField] private Player playerReference;
    public Player PlayerReference
    {
        get { return playerReference; }
        set
        {
            playerReference = value;
            cinemachineCamera.Target.TrackingTarget = playerReference.transform;
        }
    }

    [SerializeField] private Camera playerCamera;
    public Camera PlayerCamera { get => playerCamera; set => playerCamera = value; }

    [SerializeField] private Transform playerSpawnAnchor;
    public Transform PlayerSpawnAnchor { get => playerSpawnAnchor; set => playerSpawnAnchor = value; }

    private int iterationCount;
    public int IterationCount { get => iterationCount; set => iterationCount = value; }

    private void Awake()
    {
        SingletonSetup();
        FetchDependencies();
    }

    private void FetchDependencies()
    {
        cinemachineCamera = FindAnyObjectByType<CinemachineCamera>();
    }

}