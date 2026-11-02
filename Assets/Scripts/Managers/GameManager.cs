using UnityEngine;
using UnityEngine.Rendering;

public class GameManager : MonoBehaviour
{

    private static GameManager instance;
    public static GameManager Instance
    {
        get { return instance; }
    }

    public Vector3 boundsUpper = new(100f, 100f, 100f);
    public Vector3 boundsLower = new(-100f, -100f, -100f);

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
        QDebugManager.Instance.Mild(this, "Exit entered");
        QSceneManager.instance.EndScene();
    }


}
