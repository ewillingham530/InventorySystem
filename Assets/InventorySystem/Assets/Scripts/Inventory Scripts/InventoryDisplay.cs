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

    
   

}
