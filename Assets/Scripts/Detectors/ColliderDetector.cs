using UnityEngine;

public class ColliderDetector : Detector
{
    [Header("On")]
    public bool triggerEnter;
    public bool collisionEnter;



    private void OnTriggerEnter(Collider other)
    {
        QDebugManager.Instance.Mild(this, other.name + " entered the trigger of " + name);

        if (!triggerEnter)
        {
            QDebugManager.Instance.Mild(this, "Rejected " + other.name + ", ontriggerenter is disabled.");
            return;
        }

        LogCheck();
        GameObjectDetected(other.gameObject);
    }

    private void OnTriggerStay(Collider other)
    {
        QDebugManager.Instance.Mild(this, other.name + " is still the trigger of " + name);

        if (!triggerEnter)
        {
            QDebugManager.Instance.Mild(this, "Rejected " + other.name + ", ontriggerenter is disabled.");
            return;
        }

        LogCheck();
        GameObjectDetected(other.gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        QDebugManager.Instance.Mild(this, collision.gameObject.name + " entered the collider of " + name);

        if (!collisionEnter)
        {
            QDebugManager.Instance.Mild(this, "Rejected " + collision.gameObject.name + ", oncollisionenter is disabled.");
            return;
        }

        LogCheck();
        GameObjectDetected(collision.gameObject);
    }

    private void OnCollisionStay(Collision collision)
    {
        QDebugManager.Instance.Mild(this, collision.gameObject.name + " is still in the collider of " + name);

        if (!collisionEnter)
        {
            QDebugManager.Instance.Mild(this, "Rejected " + collision.gameObject.name + ", oncollisionenter is disabled.");
            return;
        }

        LogCheck();
        GameObjectDetected(collision.gameObject);
    }

}
