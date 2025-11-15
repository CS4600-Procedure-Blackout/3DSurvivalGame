using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerInventory : MonoBehaviour
{
    [Header("General")]
    public List<itemType> inventoryList = new List<itemType>();
    public int selectedItem = -1;
    public int playerReach;
    [SerializeField] private Camera cam;
    [SerializeField] private GameObject throwObject_gameobject;


    [Space(20)]
    [Header("Keys")]
    [SerializeField] private KeyCode throwItemKey;
    [SerializeField] private KeyCode pickUpItemKey;


    [Space(20)]
    [Header("Item GameObjects (active in hand)")]
    [SerializeField] private GameObject water_item;
    [SerializeField] private GameObject aid_item;
    [SerializeField] private GameObject battery_item;
    [SerializeField] private GameObject flash_item;


    [Header("Item Prefabs (for dropping)")]
    [SerializeField] private GameObject water_prefab;
    [SerializeField] private GameObject aid_prefab;
    [SerializeField] private GameObject battery_prefab;
    [SerializeField] private GameObject flash_prefab;


    [Space(20)]
    [Header("UI")]
    [SerializeField] private Image[] inventorySlotImage = new Image[9];
    [SerializeField] private Sprite emptySlotImage;


    private Dictionary<itemType, GameObject> itemSetActive = new Dictionary<itemType, GameObject>();
    private Dictionary<itemType, GameObject> itemInstantiate = new Dictionary<itemType, GameObject>();




    void Start()
    {
        // Map item types to hand-held gameobjects
        itemSetActive.Add(itemType.water, water_item);
        itemSetActive.Add(itemType.aid, aid_item);
        itemSetActive.Add(itemType.flash, flash_item);
        itemSetActive.Add(itemType.battery, battery_item);


        // Map item types to prefabs for dropping
        itemInstantiate.Add(itemType.water, water_prefab);
        itemInstantiate.Add(itemType.aid, aid_prefab);
        itemInstantiate.Add(itemType.flash, flash_prefab);
        itemInstantiate.Add(itemType.battery, battery_prefab);


        DisableAllItems(); // Start with nothing equipped
        UpdateUI();        // 
        selectedItem = -1; // 
    }




    void Update()
    {
        // Item Pickup
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;


        if (Physics.Raycast(ray, out hitInfo, playerReach) && Input.GetKey(pickUpItemKey))
        {
            Pick item = hitInfo.collider.GetComponent<Pick>();


            if (item != null)
            {
                // Check inventory capacity
                if (inventoryList.Count >= 4)
                {
                    //Debug.Log("Inventory full! Can't pick up more items.");
                    return;
                }


                // Add item to inventory
                ItemPickup pickup = hitInfo.collider.GetComponent<ItemPickup>();
                if (pickup == null || pickup.itemScriptableObject == null)
                    return; // Avoid null references


                inventoryList.Add(pickup.itemScriptableObject.item_type);
                item.PickItem();


                UpdateUI();


                // Auto-select first item if inventory was previously empty
                if (selectedItem == -1)
                {
                    selectedItem = 0;
                    NewItemSelected();
                }
            }
        }


        // Drop item
        if (Input.GetKeyDown(throwItemKey) && inventoryList.Count > 0)
        {
            GameObject dropped = Instantiate(
                itemInstantiate[inventoryList[selectedItem]],
                throwObject_gameobject.transform.position,
                throwObject_gameobject.transform.rotation
            );

            // Apply physics force
            Rigidbody rb = dropped.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddForce(cam.transform.forward * 3f, ForceMode.Impulse);
                rb.AddTorque(Random.insideUnitSphere * 2f, ForceMode.Impulse);
            }

            inventoryList.RemoveAt(selectedItem);


            // Adjust selected index safely
            if (inventoryList.Count == 0)
            {
                selectedItem = -1;
                DisableAllItems();
            }
            else if (selectedItem >= inventoryList.Count)
            {
                selectedItem = inventoryList.Count - 1;
                NewItemSelected();
            }
            else
            {
                NewItemSelected();
            }


            UpdateUI();
        }


        // Hotbar selection (1–4)
        if (Input.GetKeyDown(KeyCode.Alpha1) && inventoryList.Count > 0)
        {
            selectedItem = 0;
            NewItemSelected();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) && inventoryList.Count > 1)
        {
            selectedItem = 1;
            NewItemSelected();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3) && inventoryList.Count > 2)
        {
            selectedItem = 2;
            NewItemSelected();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4) && inventoryList.Count > 3)
        {
            selectedItem = 3;
            NewItemSelected();
        }
    }


    // --- Helper Methods ---


    private void NewItemSelected()
    {
        DisableAllItems();


        // Prevent errors if no valid selection
        if (selectedItem < 0 || selectedItem >= inventoryList.Count)
            return;


        GameObject selectedItemGameObject = itemSetActive[inventoryList[selectedItem]];
        selectedItemGameObject.SetActive(true);
    }


    private void DisableAllItems()
    {
        water_item.SetActive(false);
        aid_item.SetActive(false);
        flash_item.SetActive(false);
        battery_item.SetActive(false);
    }


    private void UpdateUI()
    {
        for (int i = 0; i < inventorySlotImage.Length; i++)
        {
            if (i < inventoryList.Count)
            {
                itemType type = inventoryList[i];
                inventorySlotImage[i].sprite =
                    itemSetActive[type].GetComponent<Item>().itemScriptableObject.item_sprite;
            }
            else
            {
                inventorySlotImage[i].sprite = emptySlotImage;
            }
        }
    }
}


public interface Pick
{
    void PickItem();
}

