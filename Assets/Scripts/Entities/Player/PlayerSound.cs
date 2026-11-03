using UnityEngine;

/// <summary>
/// WIP
/// </summary>
[RequireComponent(typeof(AudioSource))]
public class PlayerSound : MonoBehaviour
{
    private AudioSource audioSource;

    private void Awake()
    {
        FetchDependencies();
    }

    private void Update()
    {
        TriggerJumpSound();
    }

    private void FetchDependencies()
    {
        audioSource = GetComponent<AudioSource>();
    }


    public void TriggerJumpSound()
    {
        if (audioSource.isPlaying)
        {
            return;
        }
        audioSource.Play();
    }

}
