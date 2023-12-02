using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItemController : MonoBehaviour
{
    Item item;

    public Button RemoveItemButton;

    // Removes the item from both the list and the content
    public void RemoveItem()
    {
        InventoryManager.Instance.RemoveItem(item);

        Destroy(gameObject);
    }

    public void AddItem(Item newItem)
    {
        item = newItem;
    }
}
