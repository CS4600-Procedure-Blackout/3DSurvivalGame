using UnityEngine;
using System.Collections;

public class FlickeringLight : MonoBehaviour
{
    private Light lightSource;
    private float baseIntensity;

    [Header("Base Light")]
    public float normalIntensity = 1.5f;      // normal brightness when stable

    [Header("Time Between Flicker Bursts (seconds)")]
    public float minTimeBetweenBursts = 2f; // shortest calm time
    public float maxTimeBetweenBursts = 7f; // longest calm time

    [Header("Flickers per Burst")]
    public int minFlickers = 2;            // min number of flashes in a burst
    public int maxFlickers = 6;            // max number of flashes in a burst

    [Header("Inside Burst Timing (seconds)")]
    public float minOffTime = 0.03f;       // how long the light stays OFF each flicker
    public float maxOffTime = 0.12f;

    public float minOnTime = 0.04f;        // how long the light stays ON between offs
    public float maxOnTime = 0.18f;

    [Header("On Intensity Jitter")]
    public float intensityJitter = 0.4f;   // small random shake around normalIntensity

    private void Start()
    {
        lightSource = GetComponent<Light>();

        baseIntensity = normalIntensity;
        lightSource.intensity = baseIntensity;

        StartCoroutine(FlickerRoutine());
    }

    IEnumerator FlickerRoutine()
    {
        while (true)
        {
            // 1. Wait calmly before the next burst
            float waitTime = Random.Range(minTimeBetweenBursts, maxTimeBetweenBursts);
            yield return new WaitForSeconds(waitTime);

            // 2. Pick how many times it will flicker this burst
            int flickerCount = Random.Range(minFlickers, maxFlickers + 1);

            for (int i = 0; i < flickerCount; i++)
            {
                // OFF (instant)
                lightSource.intensity = 0f;
                float offTime = Random.Range(minOffTime, maxOffTime);
                yield return new WaitForSeconds(offTime);

                // ON (instant, with small random jitter)
                float jittered = baseIntensity + Random.Range(-intensityJitter, intensityJitter);
                lightSource.intensity = Mathf.Max(0f, jittered);

                float onTime = Random.Range(minOnTime, maxOnTime);
                yield return new WaitForSeconds(onTime);
            }

            // 3. After the burst, ensure it returns to stable normal brightness
            lightSource.intensity = baseIntensity;
        }
    }
}