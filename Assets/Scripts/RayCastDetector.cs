using UnityEngine;

public class RayCastDetector : Detector
{
    public float maxDistance;
    public Vector3 direction;
    public LayerMask targetLayers;
    public bool debug;
    [Tooltip("Specifies whether this query should hit Triggers.")]
    public QueryTriggerInteraction queryTriggerInteraction;
    private Ray ray;

    private void Update()
    {
        ray = new(transform.position, direction);
        if (debug) Debug.DrawRay(transform.position, direction);
        SendRay();
    }

    private void SendRay()
    {
        if (Physics.Raycast(ray, out RaycastHit hitInfo, maxDistance, targetLayers, queryTriggerInteraction))
        {
            if (debug) Debug.Log("Ray " + gameObject.name + " hit " + hitInfo.collider.gameObject.name);
            if (GameObjectPassesFilter(hitInfo.collider.gameObject))
            {
                InvokeAllActions();
            }
        }
    }

}
