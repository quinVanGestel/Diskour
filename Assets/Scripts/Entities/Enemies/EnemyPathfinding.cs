using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyPathfinding : MonoBehaviour
{
    public Vector3 playerLocation;
    private NavMeshAgent navMeshAgent;
    public bool playerReachable;

    private void Awake()
    {
        GetReferences();
    }

    private void GetReferences()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        playerLocation = GameState.Instance.PlayerReference.transform.position;
        navMeshAgent.destination = playerLocation;
    }
}
