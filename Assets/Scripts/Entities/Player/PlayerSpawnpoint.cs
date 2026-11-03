using UnityEngine;

public class PlayerSpawnpoint : MonoBehaviour
{
    # region Singleton
    private static PlayerSpawnpoint instance;
    public static PlayerSpawnpoint Instance { get => instance; }

    private void SingletonSetup()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }

    #endregion

    private void Awake()
    {
        SingletonSetup();
    }
}
