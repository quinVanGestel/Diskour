using System;
using UnityEngine;
using UnityEngine.Events;

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
            Destroy(this);
            return;
        }
        instance = this;
    }
    #endregion

    private PlayerJump playerJumpReference;
    private PlayerSound playerSoundReference;

    private void Awake()
    {
        SingletonSetup();
    }

    private void Start()
    {
        GameState.Instance.onPlayerReferenceChange += FetchPlayerDependencies;
        FetchPlayerDependencies();
    }

    private void FetchPlayerDependencies()
    {
        playerJumpReference = GameState.Instance.PlayerReference.GetComponent<PlayerJump>();
        playerSoundReference = GameState.Instance.PlayerReference.GetComponent<PlayerSound>();
        playerJumpReference.onJump += TriggerJumpSound;
    }

    private void TriggerJumpSound()
    {
        if (playerSoundReference.AudioSource.isPlaying)
        {
            return;
        }

        playerSoundReference.AudioSource.Play();
    }




}
