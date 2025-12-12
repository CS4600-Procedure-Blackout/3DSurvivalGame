using System.Collections.Generic;
using UnityEngine;

public class SanityAudioManager : MonoBehaviour
{
    public static SanityAudioManager Instance { get; private set; }

    [Header("Audio")]
    public AudioSource audioSource;

    public AudioClip audio1;   // for sanity < 90
    public AudioClip audio2;   // for sanity < 80
    public AudioClip audio3;   // for sanity < 70

    [Header("Timing (seconds)")]
    public float minDelayBetweenSounds = 8f;
    public float maxDelayBetweenSounds = 20f;

    private float nextPlayTime = 0f;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        ScheduleNext();
    }

    private void ScheduleNext()
    {
        nextPlayTime = Time.time + Random.Range(minDelayBetweenSounds, maxDelayBetweenSounds);
    }

    private void Update()
    {
        if (PlayerState.Instance == null) return;
        if (audioSource == null) return;

        // wait until it's time for the next "sanity event"
        if (Time.time < nextPlayTime) return;

        float sanity = PlayerState.Instance.currentSanity;

        bool playedSomething = false;

        // Layered behaviour:
        // as sanity gets lower, more sounds are added ON TOP,
        // instead of choosing just one.
        if (sanity < 80f && audio1 != null)
        {
            audioSource.PlayOneShot(audio1);
            playedSomething = true;
        }

        if (sanity < 60f && audio2 != null)
        {
            audioSource.PlayOneShot(audio2);
            playedSomething = true;
        }

        if (sanity < 40f && audio3 != null)
        {
            audioSource.PlayOneShot(audio3);
            playedSomething = true;
        }

        ScheduleNext();
    }

    // Called when reading a note
    public void PlayNoteReadSound()
    {
        if (PlayerState.Instance == null) return;
        if (audioSource == null) return;

        float sanity = PlayerState.Instance.currentSanity;

        // lower sanity = more sounds triggered when reading a note.
        if (sanity < 40f)
        {
            if (audio2 != null) audioSource.PlayOneShot(audio2);
            if (audio3 != null) audioSource.PlayOneShot(audio3);
        }
        else if (sanity < 60f)
        {
            if (audio2 != null) audioSource.PlayOneShot(audio2);
        }
        else if (sanity < 80f)
        {
            if (audio1 != null) audioSource.PlayOneShot(audio1);
        }
    }
}
