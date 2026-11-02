using System.ComponentModel;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QSceneManager : MonoBehaviour
{
    public static QSceneManager instance;
    [Tooltip("Set to -1 to disable.")]
    [SerializeField] private int nextSceneId = -1;

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

    public void EndScene()
    {
        if (nextSceneId == -1)
        {
            Debug.LogWarning("No next scene assigned.");
            return;
        }

        SceneManager.LoadScene(nextSceneId);
    }


}
