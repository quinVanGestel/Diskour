using UnityEngine;

/// <summary>
/// WIP
/// </summary>
[RequireComponent(typeof(AudioSource))]
public class PlayerSound : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioSource AudioSource { get => audioSource; }

    private void Awake()
    {
        FetchDependencies();
    }

    private void FetchDependencies()
    {
        audioSource = GetComponent<AudioSource>();
    }
}
