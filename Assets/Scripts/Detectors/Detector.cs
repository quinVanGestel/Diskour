using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;

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

    [Header("Misc")]
    public bool targetDetectedLastCheck;
    /// <summary>
    /// A bit finicky with collider detectors, as they are exclusively reactive. They do not make checks every frame.
    /// </summary>
    public float timeSinceLastCheck;

    /// <summary>
    /// Will increase when disabled, will decrease when enabled. Set to 0 on a DetectionEnabled state change.
    /// </summary>
    public float timeSinceDisabled;

    private bool detectionEnabled;
    public bool DetectionEnabled
    {
        get { return detectionEnabled; }
        set
        {
            detectionEnabled = value;
            timeSinceDisabled = 0f;
            QDebugManager.Instance.Verbose(this, "detectionEnabled set to " + value);
        }
    }

    private void Update()
    {
        timeSinceLastCheck += Time.deltaTime;

        if (!DetectionEnabled) timeSinceDisabled += Time.deltaTime;
        else timeSinceDisabled -= Time.deltaTime;
    }

    public void InvokeAllActions()
    {
        actions.Invoke();
        QDebugManager.Instance.Mild(this, "Invoked all actions.");
    }

    protected void GameObjectDetected(GameObject detectedGameObject)
    {
        if (GameObjectPassesFilter(detectedGameObject))
        {
            targetDetectedLastCheck = true;
            InvokeAllActions();
        }
    }

    /// <summary>
    /// Make sure to call this BEFORE calling GameObjectDetected!
    /// </summary>
    protected void LogCheck()
    {
        QDebugManager.Instance.Trace(this, "detector " + gameObject.name + " ran a check");
        timeSinceLastCheck = 0f;
        targetDetectedLastCheck = false;
    }


    public virtual bool GameObjectPassesFilter(GameObject otherGameObject)
    {
        if (tagsAllowed.Length != 0 && !QTools.AnyStringMatches(new string[] { otherGameObject.tag }, tagsAllowed))
        {
            QDebugManager.Instance.Verbose(this, "Rejected " + otherGameObject.name + ", its tag is not explicitly allowed.");
            return false;
        }

        if (tagsBanned.Length != 0 && QTools.AnyStringMatches(new string[] { otherGameObject.tag }, tagsBanned))
        {
            QDebugManager.Instance.Verbose(this, "Rejected " + otherGameObject.name + ", its tag is banned.");
            return false;
        }

        if (namesAllowed.Length != 0 && !QTools.AnyStringMatches(new string[] { otherGameObject.name }, namesAllowed))
        {
            QDebugManager.Instance.Verbose(this, "Rejected " + otherGameObject.name + ", its name is not explicitly allowed.");
            return false;
        }

        if (namesBanned.Length != 0 && QTools.AnyStringMatches(new string[] { otherGameObject.name }, namesBanned))
        {
            QDebugManager.Instance.Verbose(this, "Rejected " + otherGameObject.name + ", its name is banned.");
            return false;
        }

        // QDebugManager.Trace(this, componentWhitelist[0].name);
        if (componentWhitelist.Length != 0 && !QTools.AnyMonoBehaviourMatches(otherGameObject.GetComponents<Component>(), componentWhitelist))
        {
            QDebugManager.Instance.Verbose(this, "Rejected " + otherGameObject.name + ", none of its components are explicitly allowed..");
            return false;
        }

        // QDebugManager.Trace(this, componentBlacklist[0].name);
        if (componentBlacklist.Length != 0 && QTools.AnyMonoBehaviourMatches(otherGameObject.GetComponents<Component>(), componentBlacklist))
        {
            QDebugManager.Instance.Verbose(this, "Rejected " + otherGameObject.name + ", one of its components is banned.");
            return false;
        }

        return true;
    }

}
