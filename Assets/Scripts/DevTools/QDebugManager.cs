using System;
using System.Threading.Tasks;
using UnityEngine;

public enum ELogLevels
{
    None, Severe, Important, Mild, Verbose, Trace
}


public class QDebugManager : MonoBehaviour
{


    [Serializable]
    public class CategoryLogLevel
    {
        public string categoryName;
        public ELogLevels logLevel;
        public string[] typeNames;

        public CategoryLogLevel(string _categoryName = "unnamed", ELogLevels _logLevel = ELogLevels.None)
        {
            categoryName = _categoryName;
            logLevel = _logLevel;
        }
    }

    private static QDebugManager instance;
    public static QDebugManager Instance => instance;

    public bool warnIfUnableToFindLogLevel;

    public CategoryLogLevel[] categoryLogLevels;


    private void Awake()
    {
        SingletonSetup();
    }

    private CategoryLogLevel ComponentCategory(Component component)
    {
        Type receivedComponentType = component.GetType();

        foreach (CategoryLogLevel categoryLogLevel in categoryLogLevels)
        {
            foreach (string typeName in categoryLogLevel.typeNames)
            {
                Type categoryType = Type.GetType(typeName);
                if (categoryType == receivedComponentType)
                {
                    return categoryLogLevel;
                }
            }
        }

        if (warnIfUnableToFindLogLevel)
        {
            Debug.LogWarning("QDebugManager could not find the loglevel of " + receivedComponentType.Name);
        }
        
        CategoryLogLevel fallBackCategory = new();
        return fallBackCategory;
    }

    private string FilterContent(Component component, string content)
    {
        content = component.gameObject.name + "'s " + ComponentCategory(component).categoryName + ":\n" + content;
        return content;
    }

    public void Severe(Component component, string content)
    {
        content = FilterContent(component, content);
        if (ComponentCategory(component).logLevel >= ELogLevels.Severe)
        {
            Debug.LogError(content);
        }
    }

    public void Important(Component component, string content)
    {
        content = FilterContent(component, content);
        if (ComponentCategory(component).logLevel >= ELogLevels.Important)
        {
            Debug.LogWarning(content);
        }
    }

    public void Mild(Component component, string content)
    {
        content = FilterContent(component, content);
        if (ComponentCategory(component).logLevel >= ELogLevels.Mild)
        {
            Debug.Log(content);
        }
    }

    public void Verbose(Component component, string content)
    {
        content = FilterContent(component, content);
        if (ComponentCategory(component).logLevel >= ELogLevels.Verbose)
        {
            Debug.Log(content);
        }
    }

    public void Trace(Component component, string content)
    {
        content = FilterContent(component, content);
        if (ComponentCategory(component).logLevel >= ELogLevels.Trace)
        {
            Debug.Log(content);
        }
    }

    private void SingletonSetup()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }

}
