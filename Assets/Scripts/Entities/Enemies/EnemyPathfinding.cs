using UnityEngine;

public class EnemyPathfinding : MonoBehaviour
{
    public Vector3 PlayerLocation { get => playerReference.transform.position; }

    [Tooltip("Set to -1 to target current iteration.")]
    public int targetIteration = -1;
    private GameObject playerReference;


    private void Awake()
    {
        FetchDependencies();
    }

    private void Update()
    {
        TryFetchPlayerReference();
    }

    private void FetchDependencies()
    {
        TryFetchPlayerReference();
    }

    private void TryFetchPlayerReference()
    {
        bool fetchPlayerAllowed = targetIteration == GameState.Instance.IterationCount || targetIteration == -1;
        if (!fetchPlayerAllowed)
        {
            return;
        }
        playerReference = GameState.Instance.PlayerReference.gameObject;
    }

}
