using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework.Internal;
using UnityEngine;
using UnityEngine.Events;



// [Serializable]
// public class SoundCategory : MonoBehaviour
// {

//     public bool testBool;

//     public Action[] triggerActions;
//     [SerializeField] private AudioClip[] audioClips;
//     [SerializeField] private AudioSource audioSource;
//     // [SerializeField] private bool shuffleIfAllPlayed;
//     // [SerializeField] private bool placePlayedSoundAtEndOfQueue;
//     // [SerializeField] private bool playAllConsecutively;

//     private List<AudioClip> liveQueue;
//     private List<AudioClip> backingQueue;
//     // private int amountToPlay;


//     private void Update()
//     {


//         TryPlayFirstInQueue();
//     }

//     private void TryPlayFirstInQueue()
//     {
//         if (!AllowedAndAbleToPlay())
//         {
//             return;
//         }

//         PlayClip(liveQueue[0]);
//     }

//     private bool AllowedAndAbleToPlay()
//     {
//         if (liveQueue.Count == 0)
//         {
//             return false;
//         }

//         if (audioSource.isPlaying)
//         {
//             return false;
//         }

//         if (!SoundManager.Instance.MayIPlay(this))
//         {
//             return false;
//         }

//         return true;
//     }

//     private void PlayClip(AudioClip clip)
//     {
//         audioSource.clip = clip;
//         audioSource.Play();
//     }

//     public void Trigger()
//     {
//         if (audioClips.Length == 0)
//         {
//             backingQueue = audioClips.ToList();
//         }

//         liveQueue.Add(backingQueue[0]);
//         backingQueue.RemoveAt(0);
//     }
// }



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

    [SerializeField] private AudioClip jumpSound;
    [SerializeField] private AudioClip deathSound;
    [SerializeField] private AudioClip victorySound;

    // public SoundCategory[] soundCategories;

    private Player playerReference;

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


        playerSoundReference = GameState.Instance.PlayerReference.GetComponent<PlayerSound>();

        playerReference = GameState.Instance.PlayerReference;
        playerReference.onDeath += TriggerDeathSound;


        playerJumpReference = GameState.Instance.PlayerReference.GetComponent<PlayerJump>();
        playerJumpReference.onJump += TriggerJumpSound;
    }

    public void TriggerVictorySound()
    {
        playerSoundReference.AudioSource.Stop();
        playerSoundReference.AudioSource.clip = victorySound;
        playerSoundReference.AudioSource.Play();
    }

    private void TriggerDeathSound()
    {
        playerSoundReference.AudioSource.Stop();
        playerSoundReference.AudioSource.clip = deathSound;
        playerSoundReference.AudioSource.Play();
    }

    private void TriggerJumpSound()
    {
        if (playerSoundReference.AudioSource.isPlaying)
        {
            return;
        }

        playerSoundReference.AudioSource.clip = jumpSound;
        playerSoundReference.AudioSource.Play();
    }

    // public bool MayIPlay(SoundCategory caller)
    // {
    //     //stub :)
    //     return true;
    // }


}
