using UnityEngine;

/// <summary>
/// WIP
/// </summary>
class SoundManager : MonoBehaviour
{

    # region Singleton
    private static SoundManager instance;
    public static SoundManager Instance { get => instance; }

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
