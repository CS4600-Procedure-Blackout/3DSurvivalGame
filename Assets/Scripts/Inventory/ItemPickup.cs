using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour, Pick
{
    public ItemSO itemScriptableObject;
    
    public void PickItem()
    {
        Destroy(gameObject);
    }
}
