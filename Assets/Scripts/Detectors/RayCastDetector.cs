using UnityEngine;

public class RayCastDetector : Detector
{
    public float maxDistance;
    public Vector3 direction;
    public LayerMask targetLayers;
    public bool debug;
    [Tooltip("Specifies whether this query should hit Triggers.")]
    public QueryTriggerInteraction queryTriggerInteraction;

    private void Update()
    {
        if (DetectionEnabled) SendRay();
    }

    private void SendRay()
    {
        LogCheck();

        Ray ray = new(transform.position, direction);
        if (debug) Debug.DrawRay(transform.position, direction);

        if (Physics.Raycast(ray, out RaycastHit hitInfo, maxDistance, targetLayers, queryTriggerInteraction))
        {
            QDebugManager.Instance.Verbose(this, "hit " + hitInfo.collider.gameObject.name);
            GameObjectDetected(hitInfo.collider.gameObject);
        }
    }

}
