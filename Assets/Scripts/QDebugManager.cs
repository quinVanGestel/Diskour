using System;
using UnityEngine;
using UnityEngine.Rendering;

public enum ELogLevels
{
    None, Severe, Important, Mild, Verbose, Trace
}

[System.Serializable]
public class TypeLogLevel
{
    [Tooltip("Any one of that type will do! Which specific game object you get it from should not matter at all.")]
    public MonoBehaviour component;
    public ELogLevels logLevel;
}

public class QDebugManager : MonoBehaviour
{

    private static QDebugManager instance;
    public static QDebugManager Instance
    {
        get { return instance; }
    }

    public TypeLogLevel[] typeLogLevels;

    private void Awake()
    {
        SingletonSetup();
    }

    private ELogLevels ComponentLogLevel(Component component)
    {
        System.Type receivedComponentType = component.GetType();

        foreach (TypeLogLevel typeLogLevel in typeLogLevels)
        {
            Type componentType = typeLogLevel.component.GetType();
            if (componentType == receivedComponentType)
            {
                return typeLogLevel.logLevel;
            }
        }

        Debug.LogWarning("QDebugManager could not find the loglevel of " + component.name);
        return ELogLevels.None;
    }

    public void Severe(Component component, string content)
    {
        if (ComponentLogLevel(component) >= ELogLevels.Severe)
        {
            Debug.LogError(content);
        }
    }

    public void Important(Component component, string content)
    {
        if (ComponentLogLevel(component) >= ELogLevels.Important)
        {
            Debug.LogWarning(content);
        }
    }

    public void Mild(Component component, string content)
    {
        if (ComponentLogLevel(component) >= ELogLevels.Mild)
        {
            Debug.Log(content);
        }
    }

    public void Verbose(Component component, string content)
    {
        if (ComponentLogLevel(component) >= ELogLevels.Verbose)
        {
            Debug.Log(content);
        }
    }

    public void Trace(Component component, string content)
    {
        if (ComponentLogLevel(component) >= ELogLevels.Trace)
        {
            Debug.Log(content);
        }
    }

    private void SingletonSetup()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }

}
