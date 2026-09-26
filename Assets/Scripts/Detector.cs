using UnityEngine;
using UnityEngine.Events;

public abstract class Detector : MonoBehaviour
{

    [Header("If")]
    public string[] tagsAllowed;
    public string[] tagsBanned;

    public string[] namesAllowed;
    public string[] namesBanned;

    public Component[] componentWhitelist;
    public Component[] componentBlacklist;

    [Header("Then")]
    public UnityEvent actions;

    public void InvokeAllActions()
    {
        // foreach (UnityEvent action in actions)
        // {
        actions.Invoke();
        // }
        QDebugManager.Mild(this, "Invoked all actions.");
    }

    public virtual bool GameObjectPassesFilter(GameObject otherGameObject)
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

        // QDebugManager.Trace(this, componentWhitelist[0].name);
        if (componentWhitelist.Length != 0 && !QTools.AnyMonoBehaviourMatches(otherGameObject.GetComponents<Component>(), componentWhitelist))
        {
            QDebugManager.Mild(this, "Rejected " + otherGameObject.name + ", none of its components are explicitly allowed..");
            return false;
        }

        // QDebugManager.Trace(this, componentBlacklist[0].name);
        if (componentBlacklist.Length != 0 && QTools.AnyMonoBehaviourMatches(otherGameObject.GetComponents<Component>(), componentBlacklist))
        {
            QDebugManager.Mild(this, "Rejected " + otherGameObject.name + ", one of its components is banned.");
            return false;
        }

        return true;
    }

}
