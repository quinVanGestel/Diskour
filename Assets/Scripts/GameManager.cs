using UnityEngine;
using UnityEngine.Rendering;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;

    private void Awake()
    {
        SingletonSetup();
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

    public void ExitEntered()
    {
        QDebugManager.Mild(this, "Exit entered");
    }

}
