using UnityEngine;
using UnityEngine.Events;

public class Detector : MonoBehaviour
{
    [Header("On")]
    public bool triggerEnter;
    public bool collisionEnter;

    [Header("If")]
    public string[] tagsAllowed;
    public string[] tagsBanned;

    public string[] namesAllowed;
    public string[] namesBanned;

    public Component[] monoBehaviourWhitelist;
    public Component[] monoBehaviourBlacklist;

    [Header("Then")]
    public UnityEvent[] actions;

    private void InvokeAllActions()
    {
        foreach (UnityEvent action in actions)
        {
            action.Invoke();
        }
        QDebugManager.Mild(this, "Invoked all actions.");
    }

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

    private bool GameObjectPassesFilter(GameObject otherGameObject)
    {
        if (tagsAllowed.Length != 0 && !QTools.AnyStringMatches(new string[] { otherGameObject.tag }, tagsAllowed))
        {
            QDebugManager.Mild(this, "Rejected " + otherGameObject.name + ", its tag is not explicitly allowed.");
            return false;
        }

        if (tagsBanned.Length != 0 && QTools.AnyStringMatches(new string[] { otherGameObject.tag }, tagsBanned))
        {
            QDebugManager.Mild(this, "Rejected " + otherGameObject.name + ", its tag is banned.");
        }

        if (namesAllowed.Length != 0 && !QTools.AnyStringMatches(new string[] { otherGameObject.name }, namesAllowed))
        {
            QDebugManager.Mild(this, "Rejected " + otherGameObject.name + ", its name is not explicitly allowed.");
            return false;
        }

        if (namesBanned.Length != 0 && QTools.AnyStringMatches(new string[] { otherGameObject.name }, namesBanned))
        {
            QDebugManager.Mild(this, "Rejected " + otherGameObject.name + ", its name is banned.");
            return false;
        }

        QDebugManager.Trace(this, monoBehaviourWhitelist[0].name);
        if (monoBehaviourWhitelist.Length != 0 && !QTools.AnyMonoBehaviourMatches(otherGameObject.GetComponents<Component>(), monoBehaviourWhitelist))
        {
            QDebugManager.Mild(this, "Rejected " + otherGameObject.name + ", none of its components are explicitly allowed..");
            return false;
        }

        QDebugManager.Trace(this, monoBehaviourBlacklist[0].name);
        if (monoBehaviourBlacklist.Length != 0 && QTools.AnyMonoBehaviourMatches(otherGameObject.GetComponents<Component>(), monoBehaviourBlacklist))
        {
            QDebugManager.Mild(this, "Rejected " + otherGameObject.name + ", one of its components is banned.");
            return false;
        }

        return true;
    }



}
