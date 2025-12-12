using UnityEngine;

public class BackgroundMusicManager : MonoBehaviour
{
    [Header("Audio")]
    public AudioSource audioSource;      // 2D audio source
    public AudioClip normalMusic;        // plays at start
    public AudioClip lowSanityMusic;     // plays when sanity <= threshold

    [Header("Sanity Threshold")]
    public float sanityThreshold = 50f;  // switch music when sanity <= this

    private bool usingLowSanityMusic = false;

    private void Start()
    {
        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();

        if (audioSource != null && normalMusic != null)
        {
            audioSource.clip = normalMusic;
            audioSource.loop = true;
            audioSource.Play();
        }
    }

    private void Update()
    {
        if (PlayerState.Instance == null || audioSource == null)
            return;

        // if it's already swapped? then do nothing
        if (usingLowSanityMusic)
            return;

        // when sanity drops to threshold or below, switch track once
        if (PlayerState.Instance.currentSanity <= sanityThreshold)
        {
            usingLowSanityMusic = true;

            if (lowSanityMusic != null)
            {
                audioSource.clip = lowSanityMusic;
                audioSource.loop = true;
                audioSource.Play();
            }
        }
    }
}