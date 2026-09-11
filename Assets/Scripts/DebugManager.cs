using UnityEngine;

public enum LogLevels
{
    None, Severe, Important, Mild, Verbose, Trace
}

public static class QDebugManager
{
    [Header("Categories")]
    public static LogLevels detector = LogLevels.Verbose;

    private static LogLevels ComponentLogLevel(Component component)
    {
        if (component.GetType() == typeof(Detector))
        {
            return detector;
        }

        Debug.LogWarning("QDebugManager could not find the loglevel of " + component.name);
        return LogLevels.None;
    }

    public static void Severe(Component component, string content)
    {
        if (ComponentLogLevel(component) >= LogLevels.Severe)
        {
            Debug.LogError(content);
        }
    }

    public static void Important(Component component, string content)
    {
        if (ComponentLogLevel(component) >= LogLevels.Important)
        {
            Debug.LogWarning(content);
        }
    }

    public static void Mild(Component component, string content)
    {
        if (ComponentLogLevel(component) >= LogLevels.Mild)
        {
            Debug.Log(content);
        }
    }

    public static void Verbose(Component component, string content)
    {
        if (ComponentLogLevel(component) >= LogLevels.Verbose)
        {
            Debug.Log(content);
        }
    }

    public static void Trace(Component component, string content)
    {
        if (ComponentLogLevel(component) >= LogLevels.Trace)
        {
            Debug.Log(content);
        }
    }

}
