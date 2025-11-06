using UnityEngine;

public class AI_Movement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 0.8f;              // a bit faster so the walk looks natural
    public float turnSpeed = 360f;              // degrees per second for smooth turning

    [Header("Timing (randomized every cycle)")]
    public Vector2 walkTimeRange = new Vector2(1.6f, 3.0f);
    public Vector2 waitTimeRange = new Vector2(0.15f, 0.60f);
    [Range(0f, 1f)] public float skipWaitChance = 0.35f; // sometimes skip the idle pause entirely

    // --- internals ---
    Animator animator;
    Rigidbody rb;

    float walkTime, walkCounter;
    float waitTime, waitCounter;

    int walkDirection;          // 0=+Z, 1=+X, 2=-X, 3=-Z
    float targetYaw;            // where we want to face
    bool isWalking;

    void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        RandomizeWalkTime();
        RandomizeWaitTime();
        waitCounter = waitTime;
        walkCounter = walkTime;
        ChooseDirection();
    }

    void Update()
    {
        if (isWalking)
        {
            // drive animation
            if (animator) animator.SetBool("isRunning", true);

            // smooth rotate toward target heading
            var targetRot = Quaternion.Euler(0f, targetYaw, 0f);
            transform.rotation = Quaternion.RotateTowards(
                transform.rotation, targetRot, turnSpeed * Time.deltaTime);

            // move forward in the facing direction
            Vector3 step = transform.forward * moveSpeed * Time.deltaTime;
            if (rb != null && !rb.isKinematic)
                rb.MovePosition(rb.position + step);
            else
                transform.position += step;

            // countdown this walk burst
            walkCounter -= Time.deltaTime;
            if (walkCounter <= 0f)
            {
                isWalking = false;
                if (animator) animator.SetBool("isRunning", false);

                // sometimes skip waiting to feel more alive
                if (Random.value < skipWaitChance)
                {
                    RandomizeWalkTime();
                    ChooseDirection();
                }
                else
                {
                    RandomizeWaitTime();
                    waitCounter = waitTime;
                }
            }
        }
        else
        {
            // idle countdown
            waitCounter -= Time.deltaTime;
            if (waitCounter <= 0f)
            {
                RandomizeWalkTime();
                ChooseDirection();
            }
        }
    }

    void ChooseDirection()
    {
        walkDirection = Random.Range(0, 4);
        switch (walkDirection)
        {
            case 0:  targetYaw = 0f;   break;   // +Z
            case 1:  targetYaw = 90f;  break;   // +X
            case 2:  targetYaw = -90f; break;   // -X
            case 3:  targetYaw = 180f; break;   // -Z
        }

        isWalking = true;
        walkCounter = walkTime;
    }

    void RandomizeWalkTime() => walkTime = Random.Range(walkTimeRange.x, walkTimeRange.y);
    void RandomizeWaitTime() => waitTime = Random.Range(waitTimeRange.x, waitTimeRange.y);
}