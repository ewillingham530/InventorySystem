using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;
    public List<Item> Items = new List<Item>(); //where picked-up items will be stored

    public Transform _itemContent;       //Location where items (2D UI prefab) are filled
    public GameObject _inventoryItem;    //2D UI prefab item

    public Toggle _enableRemove;

    public InventoryItemController[] InventoryItems;

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
            GameObject obj = Instantiate(_inventoryItem, _itemContent);
            var itemName = obj.transform.Find("ItemName").GetComponent<TMP_Text>();
            var itemIcon = obj.transform.Find("ItemImage").GetComponent<Image>();
            var removeButton = obj.transform.Find("RemoveItemButton").GetComponent<Button>();

            itemName.text = item.itemName;
            itemIcon.sprite = item.icon;

            if(_enableRemove.isOn)
            {
                removeButton.gameObject.SetActive(true);
            }
        }

        SetInventoryItems();
    }

    // cleans content before open
    public void CleanList()
    {
        foreach (Transform item in _itemContent)
        {
            Destroy(item.gameObject);
        }
    }

    public void EnableItemsRemoved()
    {
        if(_enableRemove.isOn)
        {
            foreach (Transform item in _itemContent)
            {
                item.Find("RemoveItemButton").gameObject.SetActive(true);
            }
        }

        else
        {
            foreach (Transform item in _itemContent)
            {
                item.Find("RemoveItemButton").gameObject.SetActive(false);
            }
        }
    }

    public void SetInventoryItems()
    {
        InventoryItems = _itemContent.GetComponentsInChildren<InventoryItemController>();

        for (int i = 0; i < Items.Count; i++)
        {
            InventoryItems[i].AddItem(Items[i]);
        }
    }
}
