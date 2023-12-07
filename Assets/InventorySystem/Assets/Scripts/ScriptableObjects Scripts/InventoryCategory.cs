using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//create menu item
[CreateAssetMenu(menuName = "Inventory/Inventory Category", fileName = "InventoryCategory", order = 2)]

public class InventoryCategory : ScriptableObject
{
    [SerializeField] public int Id;
    [SerializeField] public string Name;

}
