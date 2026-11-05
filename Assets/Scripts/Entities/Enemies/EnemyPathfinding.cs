using UnityEngine;

public class EnemyPathfinding : MonoBehaviour
{

    /// <summary>
    /// If PlayerLocationKnown false, returns position of gameobject on this class' instance.
    /// </summary>
    public Vector3 PlayerLocation
    {
        get
        {
            if (!PlayerLocationKnown)
            {
                return transform.position;
            }

            return playerReference.transform.position;
        }
    }

    public bool PlayerLocationKnown
    {
        get
        {
            return playerReference != null;
        }
    }

    [Tooltip("Set to -1 to target current iteration.")]
    public int targetIteration = -1;
    private GameObject playerReference;


    private void Start()
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
