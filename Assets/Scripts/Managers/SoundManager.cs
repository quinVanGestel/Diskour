using System;
using UnityEngine;
using UnityEngine.Events;




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

    [Serializable]
    private class SoundCategory
    {
        // [SerializeField] private [] triggerActions;
        [SerializeField] private AudioClip[] audioClips;
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private bool shuffleIfAllPlayed;
        [SerializeField] private bool placePlayedSoundAtEndOfQueue;
        [SerializeField] private bool playAllConsecutively;

        private AudioClip[] currentQueue;
        private AudioClip[] secondaryQueue;
        private int amountToPlay;


        public void AddTriggerActionsListeners()
        {
        }

        private void TriggerActionTriggered()
        {
            if (playAllConsecutively)
            {
                secondaryQueue.CopyTo(currentQueue, 0);
            }

        }


    }

    [SerializeField] private SoundCategory soundCategories;


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
