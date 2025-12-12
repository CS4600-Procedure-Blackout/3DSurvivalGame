using UnityEngine;

public class PlayerState : MonoBehaviour
{
    // Singleton 
    public static PlayerState Instance { get; private set; }

    //_______ SANITY ________
    [Header("Sanity (horror intensity)")]
    public float maxSanity = 100f;
    public float currentSanity;

    [Tooltip("Sanity drain per second at the start of the game.")]
    public float baseSanityDrainPerSecond = 0.1f;

    [Tooltip("How much the sanity drain rate increases per minute spent in the hospital.")]
    public float sanityDrainIncreasePerMinute = 0.05f;

    // Internal timer to make sanity drain faster over time
    private float timeInHospitalSeconds = 0f;


    // ________ HYDRATION _________
    [Header("Hydration (lose condition)")]
    public float maxHydration = 100f;
    public float currentHydration;

    [Tooltip("Constant hydration drain per second.")]
    public float hydrationDrainPerSecond = 0.15f;

    // we only want to trigger the 'return home' UI once
    private bool hydrationDepletedHandled = false;

    // Reference to the 'Return Home' UI controller
    public ReturnHomeUI returnHomeUI;


    // _______ FLASHLIGHT BATTERY _________
    [Header("Flashlight Battery")]
    public float maxBattery = 100f;
    public float currentBattery;

    [Tooltip("Battery drain per second while flashlight is on.")]
    public float batteryDrainPerSecond = 2f;

    [Tooltip("Is the flashlight currently on? (Set by your flashlight script)")]
    public bool flashlightOn = false;



    // ================= LIFECYCLE =================

    private void Awake()
    {
        // Singleton pattern
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        // Optional if later we decide to change scenes:
        // DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        currentSanity    = maxSanity;
        currentHydration = maxHydration;
        currentBattery   = maxBattery;   // start flashlight battery full
    }

    private void Update()
    {
        UpdateSanity();
        UpdateHydration();
        UpdateBattery();

        // Debug keys
        // if (Input.GetKeyDown(KeyCode.Alpha1)) RestoreSanity(20f);
        // if (Input.GetKeyDown(KeyCode.Alpha2)) DrinkWater(30f);
        // if (Input.GetKeyDown(KeyCode.Alpha3)) RechargeBattery(20f);
    }


    // ================= SANITY =================

    // Sanity drains forever, and faster the longer the player stays
    private void UpdateSanity()
    {
        // Count total time in the hospital (whole game)
        timeInHospitalSeconds += Time.deltaTime;
        float minutes = timeInHospitalSeconds / 60f;

        // Drain rate grows over time:
        // base + (minutes * extra per minute)
        float currentDrainRate =
            baseSanityDrainPerSecond +
            minutes * sanityDrainIncreasePerMinute;

        currentSanity -= currentDrainRate * Time.deltaTime;
        currentSanity = Mathf.Clamp(currentSanity, 0f, maxSanity);

        // sanity reaching 0 does NOT kill the player.
        // Other scripts will read currentSanity and apply
        // blur/hallucinations/audio based on this value.
    }

    // Called when player uses a medkit
    public void RestoreSanity(float amount)
    {
        currentSanity = Mathf.Clamp(currentSanity + amount, 0f, maxSanity);
    }



    // =============== HYDRATION ===============

    // Hydration drains at a constant rate; 0 = "return home" lose condition
    private void UpdateHydration()
    {
        if (hydrationDepletedHandled) return;

        currentHydration -= hydrationDrainPerSecond * Time.deltaTime;
        currentHydration = Mathf.Clamp(currentHydration, 0f, maxHydration);

        if (currentHydration <= 0f)
        {
            hydrationDepletedHandled = true;
            HandleHydrationDepleted();
        }
    }

    private void HandleHydrationDepleted()
    {
        Debug.Log("Hydration reached 0. Returning home…");

        if (returnHomeUI != null)
        {
            returnHomeUI.ShowReturnHome();
        }
        else
        {
            Debug.LogWarning("ReturnHomeUI is not assigned on PlayerState.");
        }
    }

    // Called in the WaterItem.cs script when the player drinks water
    public void DrinkWater(float amount)
    {
        currentHydration = Mathf.Clamp(currentHydration + amount, 0f, maxHydration);
    }



    // ================= FLASHLIGHT BATTERY =================

    private void UpdateBattery()
    {
        // Only drain when flashlight is ON
        if (!flashlightOn)
            return;

        if (currentBattery <= 0f)
        {
            currentBattery = 0f;
            flashlightOn = false;   // ensure state matches
            return;
        }

        currentBattery -= batteryDrainPerSecond * Time.deltaTime;
        currentBattery = Mathf.Clamp(currentBattery, 0f, maxBattery);
    }

    // Called when picking up batteries
    public void RechargeBattery(float amount)
    {
        currentBattery = Mathf.Clamp(currentBattery + amount, 0f, maxBattery);
    }

    // Called by the inventory script when toggling light
    public bool SetFlashlightState(bool on)
    {
        // If trying to turn ON but battery is empty, refuse
        if (on && currentBattery <= 0f)
        {
            flashlightOn = false;
            return false;
        }

        flashlightOn = on;
        return flashlightOn;
    }
}