using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public Item _item;

    void Pickup()
    {
        bool itemAdded = InventoryManager.Instance.AddItem(_item);
        
        if (itemAdded)
        {
            Destroy(gameObject);
        }

    }

    private void OnMouseDown()
    {
        Pickup();
    }
}
