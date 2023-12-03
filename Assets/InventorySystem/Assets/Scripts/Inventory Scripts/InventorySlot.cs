using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InventorySlot 
{
    public int ID;
    public Item slotItem;
    public int quantity;
    public InventorySlot(int id, Item item)
    {
        this.ID = id;
        slotItem = item;
        quantity = item.Quantity;
    }


}
