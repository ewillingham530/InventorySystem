using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;
    public List<Item> _itemList = new List<Item>(); //where picked-up items will be stored

    [SerializeField] public int _MaxInventorySlots = 2;
    [SerializeField] private int _stackSize;
    public int StackSize => _stackSize;

    public Transform _itemContent;       //Location where items (2D UI prefab) are filled
    public GameObject _inventoryItem;    //2D UI prefab item

    public Toggle _enableRemove;

    public InventoryItemController[] InventoryItemsSlots;

    private void Awake()
    {
        Instance = this;
    }

    // add item to List
    public bool AddItem(Item item)
    {
        if(_itemList.Count < _MaxInventorySlots)
        {
            _itemList.Add(item);
            return true;
        }
        else
        {
            Debug.Log("Max Stack Size reached. Cannot add more.");
            return false;
        }

    }

    // remove item from list
    public void RemoveItem(Item item)
    {
        _itemList.Remove(item);
    }

    // Finds and changes the name, item icon, and remove button for each Item picked up
    public void ListItems()
    {
        
        foreach (var item in _itemList)
        {
            GameObject obj = Instantiate(_inventoryItem, _itemContent);
            var itemName = obj.transform.Find("ItemName").GetComponent<TMP_Text>();
            var itemIcon = obj.transform.Find("ItemImage").GetComponent<Image>();
            var removeButton = obj.transform.Find("RemoveItemButton").GetComponent<Button>();

            itemName.text = item.ItemName;
            itemIcon.sprite = item.Icon;

            if(_enableRemove.isOn)
            {
                removeButton.gameObject.SetActive(true);
            }
        }

        SetInventoryItems();
    }

    // Cleans content before open
    public void CleanList()
    {
        foreach (Transform item in _itemContent)
        {
            Destroy(item.gameObject);
        }
    }

    // Enable the Remove Item toggle on each item in the inventory
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

    // Adds the Items to the UI when you open the inventory
    public void SetInventoryItems()
    {
        InventoryItemsSlots = _itemContent.GetComponentsInChildren<InventoryItemController>();

        for (int i = 0; i < _itemList.Count; i++)
        {
            //if ()
                InventoryItemsSlots[i].AddItem(_itemList[i]);
            
        }
    }

    
}
