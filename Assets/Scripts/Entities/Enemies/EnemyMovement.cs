using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent), typeof(EnemyPathfinding))]
public class EnemyMovement : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    private EnemyPathfinding enemyPathfinding;

    private void Awake()
    {
        FetchDependencies();
    }

    private void Update()
    {
        TryUpdateNavMeshDestination();
    }

    private void FetchDependencies()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        enemyPathfinding = GetComponent<EnemyPathfinding>();
    }

    private void TryUpdateNavMeshDestination()
    {
        if (enemyPathfinding.PlayerLocation != null)
        {
            navMeshAgent.destination = enemyPathfinding.PlayerLocation;
        }
    }

}

