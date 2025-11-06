using UnityEngine;

[RequireComponent(typeof(Collider))] 
public class AutoDoorSimple : MonoBehaviour
{
    [Header("Motion")]
    public Transform doorLeaf;          
    public float openAngle = 90f;       // how far to swing (around local Y)
    public float speed = 2f;            // swing speed

    [Header("Logic")]
    public string playerTag = "Player"; // your player MUST be tagged Player
    public bool closeOnExit = true;     // auto-close when player leaves

    // internal
    private bool inTrigger = false;
    private bool open = false;
    private Quaternion rotClosed, rotOpen;
    private Collider triggerCol;
    private Collider[] solids;          // all non-trigger colliders we’ll disable while open

    void Awake()
    {
        triggerCol = GetComponent<Collider>();
        triggerCol.isTrigger = true;                    

        if (!doorLeaf) doorLeaf = transform;           
        rotClosed = doorLeaf.localRotation;
        rotOpen   = Quaternion.Euler(
                        doorLeaf.localEulerAngles.x,
                        doorLeaf.localEulerAngles.y + openAngle,
                        doorLeaf.localEulerAngles.z);

        // collect ALL non-trigger colliders under this door 
        solids = GetComponentsInChildren<Collider>(true);
    }

    void Update()
    {
        // open while player is inside; close when they leave (if closeOnExit)
        if (inTrigger) open = true;
        else if (closeOnExit) open = false;

        // swing
        var target = open ? rotOpen : rotClosed;
        doorLeaf.localRotation = Quaternion.Slerp(doorLeaf.localRotation, target, Time.deltaTime * speed);

        // disable EVERY non-trigger collider while open, re-enable when closed
        if (solids != null)
        {
            for (int i = 0; i < solids.Length; i++)
            {
                var c = solids[i];
                if (!c || c == triggerCol || c.isTrigger) continue;
                c.enabled = !open;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag)) inTrigger = true;
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(playerTag)) inTrigger = false;
    }
}