using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InventorySlots 
{
    [SerializeField] private Item _itemData;
    [SerializeField] private int _stackSize;

    public Item ItemData => _itemData;
    public int StackSize => _stackSize;

    public InventorySlots(Item source, int amount)
    {
        _itemData = source;
        _stackSize = amount;
    }

    public InventorySlots()
    {
        ClearSlot();
    }

    public void ClearSlot()
    {
        _itemData = null;
        _stackSize = -1;
    }

    public bool RoomLeftInStack(int amountToAdd, out int amountRemaining)
    {
        amountRemaining = ItemData.MaxStackSize - _stackSize;
        return RoomLeftInStack(amountToAdd);
    }
    public bool RoomLeftInStack(int amountToAdd)
    {
        if (_stackSize + amountToAdd <= _itemData.MaxStackSize) return true;
        else return false;
    }

    public void AddToStack(int amount)
    {
        _stackSize += amount;
    }

    public void RemoveFromStack(int amount)
    {
        _stackSize -= amount;
    }

}
