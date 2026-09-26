using UnityEngine;

public class ColliderDetector : Detector
{
    [Header("On")]
    public bool triggerEnter;
    public bool collisionEnter;



    private void OnTriggerEnter(Collider other)
    {
        QDebugManager.Mild(this, other.name + " entered the trigger of " + name);

        if (!triggerEnter)
        {
            QDebugManager.Mild(this, "Rejected " + other.name + ", ontriggerenter is disabled.");
            return;
        }

        if (GameObjectPassesFilter(other.gameObject))
        {
            InvokeAllActions();
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        QDebugManager.Mild(this, collision.gameObject.name + " entered the collider of " + name);

        if (!collisionEnter)
        {
            QDebugManager.Mild(this, "Rejected " + collision.gameObject.name + ", oncollisionenter is disabled.");
            return;
        }
        if (GameObjectPassesFilter(collision.gameObject))
        {
            InvokeAllActions();
        }
    }

}
