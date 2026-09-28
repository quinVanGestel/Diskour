using System;
using UnityEngine;

public enum ELogLevels
{
    None, Severe, Important, Mild, Verbose, Trace
}


public class QDebugManager : MonoBehaviour
{


    [Serializable]
    public class TypeLogLevel
    {
        [Tooltip("Any one of that type will do! Which specific game object you get it from should not matter at all.")]
        public MonoBehaviour component;
        public ELogLevels logLevel;
    }

    [Serializable]
    public class DebugVisualisation
    {
        public Material material;
        public bool enabled;
        public MeshRenderer[] meshRenderers;
    }

    private static QDebugManager instance;
    public static QDebugManager Instance
    {
        get { return instance; }
    }

    public TypeLogLevel[] typeLogLevels;

    public DebugVisualisation[] debugVisualisations;

    private void Awake()
    {
        SingletonSetup();
    }

    private void Start()
    {
        foreach (DebugVisualisation debugVisualisation in debugVisualisations)
        {
            debugVisualisation.meshRenderers = QTools.GetAllMeshRenderers(debugVisualisation.material);
    //  QDebugManager.Instance.Trace(this, "");
        }
    }

    private void Update()
    {
        VisualisationRoutine();
    }



    private async void VisualisationRoutine()
    {
        foreach (DebugVisualisation debugVisualisation in debugVisualisations)
        {
            if (debugVisualisation.enabled)
                foreach (MeshRenderer meshRenderer in debugVisualisation.meshRenderers)
                {
                    meshRenderer.enabled = debugVisualisation.enabled;
                }
        }
    }

    private ELogLevels ComponentLogLevel(Component component)
    {
        Type receivedComponentType = component.GetType();

        foreach (TypeLogLevel typeLogLevel in typeLogLevels)
        {
            Type componentType = typeLogLevel.component.GetType();
            if (componentType == receivedComponentType)
            {
                return typeLogLevel.logLevel;
            }
        }

        Debug.LogWarning("QDebugManager could not find the loglevel of " + receivedComponentType.Name);
        return ELogLevels.None;
    }

    private string FilterContent(Component component, string content)
    {
        content = component.gameObject.name + "'s " + component.GetType().Name + ":\n" + content;
        return content;
    }

    public void Severe(Component component, string content)
    {
        content = FilterContent(component, content);
        if (ComponentLogLevel(component) >= ELogLevels.Severe)
        {
            Debug.LogError(content);
        }
    }

    public void Important(Component component, string content)
    {
        content = FilterContent(component, content);
        if (ComponentLogLevel(component) >= ELogLevels.Important)
        {
            Debug.LogWarning(content);
        }
    }

    public void Mild(Component component, string content)
    {
        content = FilterContent(component, content);
        if (ComponentLogLevel(component) >= ELogLevels.Mild)
        {
            Debug.Log(content);
        }
    }

    public void Verbose(Component component, string content)
    {
        content = FilterContent(component, content);
        if (ComponentLogLevel(component) >= ELogLevels.Verbose)
        {
            Debug.Log(content);
        }
    }

    public void Trace(Component component, string content)
    {
        content = FilterContent(component, content);
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
