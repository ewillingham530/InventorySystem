using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class InventoryDisplay : MonoBehaviour
{
    [SerializeField] MouseItemData mouseInventoryItem;

    protected Dictionary<InventoryManager, InventoryManager> slotDictionary;

    public Dictionary<InventoryManager, InventoryManager> SlotDictionary => slotDictionary;
    public abstract void AssignSlot();

    //protected virtual void UpdateSlot(InventoryManager updatedSlot)
    //{
    //    foreach (var slot in SlotDictionary)
    //    {
    //        if (slot.Value == updatedSlot)      // slot value - inventory slot
    //        {
    //            slot.Key.UpdatedUISlot(updatedSlot);       //slot key, the ui rep of the value
    //        }
    //    }
    //}

    //public void SlotClicked
   

}
