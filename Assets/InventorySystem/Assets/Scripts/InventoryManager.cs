using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;
    public List<Item> Items = new List<Item>();

    public Transform _ItemContent;       //Location where items (2D UI prefab) are filled
    public GameObject _InventoryItem;    //2D UI prefab item

    private void Awake()
    {
        Instance = this;
    }

    // add item to List
    public void AddItem(Item item)
    {
        Items.Add(item);
    }

    // remove item from list
    public void RemoveItem(Item item)
    {
        Items.Remove(item);
    }


    public void ListItems()
    {
        
        foreach (var item in Items)
        {
            GameObject obj = Instantiate(_InventoryItem, _ItemContent);
            var itemName = obj.transform.Find("ItemName").GetComponent<TMP_Text>();
            var itemIcon = obj.transform.Find("ItemImage").GetComponent<Image>();

            itemName.text = item.itemName;
            itemIcon.sprite = item.icon;
        }
    }

    // cleans content before open
    public void CleanList()
    {
        foreach (Transform item in _ItemContent)
        {
            Destroy(item.gameObject);
        }
    }
}
