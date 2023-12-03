using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]

public class Item : ScriptableObject
{
    public int ID;
    public string ItemName;         // Name of the item
    [TextArea(4, 4)]
    public string Description;      // Internal Description of item
    public bool isStackable = false;
    
    public int MaxStackSize;        // Set Max stack size per item
    public int Quantity = 1;
    public Sprite Icon;             // Item Icon

    public virtual void Use()
    {
        // use the item to make something happen

        Debug.Log("Using " + ItemName);
    }

}
