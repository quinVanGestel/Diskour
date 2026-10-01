using System;
using UnityEngine;

public class DebugVisualiser : MonoBehaviour
{

    private static DebugVisualiser instance;
    public static DebugVisualiser Instance => instance;

    [Serializable]
    public class DebugVisualisation
    {
        public Material material;
        public bool enabled;
        [Header("Readonly")]
        public MeshRenderer[] meshRenderers;
    }

    public DebugVisualisation[] debugVisualisations;

    private void Awake()
    {
    }

    private void Start()
    {
        SingletonSetup();
        InitialiseDebugVisualisations();
    }

    private void Update()
    {
        VisualisationRoutine();
    }

    private void InitialiseDebugVisualisations()
    {
        QDebugManager.Instance.Verbose(this, "Running InitialiseDebugVisualisations");
        foreach (DebugVisualisation debugVisualisation in debugVisualisations)
        {
            debugVisualisation.meshRenderers = QTools.GetAllMeshRenderers(debugVisualisation.material);
            QDebugManager.Instance.Trace(this, "Fetched all meshrenderers with material " + debugVisualisation.material.name + " length: " + debugVisualisation.meshRenderers.Length);
        }
    }

    private void VisualisationRoutine()
    {
        QDebugManager.Instance.Trace(this, "Running VisualisationRoutine");
        foreach (DebugVisualisation debugVisualisation in debugVisualisations)
        {
            foreach (MeshRenderer meshRenderer in debugVisualisation.meshRenderers)
            {
                meshRenderer.enabled = debugVisualisation.enabled;
                QDebugManager.Instance.Trace(this, "set MeshRenderer " + meshRenderer.name + " to " + debugVisualisation.enabled);
            }
        }
    }


    private void SingletonSetup()
    {
        if (instance != null && instance != this)
        {
            Debug.Log("too many singletons :(");
            Destroy(gameObject);
            return;
        }

        instance = this;
        Debug.Log("singleton setup ran without troubles :)");
    }


}
