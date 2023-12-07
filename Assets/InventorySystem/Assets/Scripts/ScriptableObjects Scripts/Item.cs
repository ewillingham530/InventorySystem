using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]

public class Item : ScriptableObject
{
    [Header("Item Identifiers")]
    public int ID;
    public string ItemName;             // Name of the item
    public Sprite Icon;                 // Item Icon

    [TextArea(4, 4)][Header("" + "")]
    public string Description;          // Internal Description of item

    [Header("" + "")][Header("Item Parameters")]
    public int Quantity = 1;
    public bool isStackable = false;    
    public int MaxStackSize;            // Set Max stack size per item
    
    [Header("Sort Category")]
    //Used to filter the objects through the Tabs in the Inventory
    [SerializeField] public InventoryCategory Category;
    public virtual void Use()
    {
       // use the item to make something happen
        Debug.Log("Using " + ItemName);
    }

}
