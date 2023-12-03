using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System.IO;
using System;

[System.Serializable]
public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;


    [SerializeField] public List<Item> _itemList = new List<Item>(); //where picked-up items will be stored

    [SerializeField] public int _MaxInventorySlots = 2;  

    public GameObject _inventory;
    public Transform _itemContent;       //Location where items (2D UI prefab) are filled
    public GameObject _inventoryItem;    //2D UI prefab item

    public Toggle _enableRemove;

    public InventoryItemController[] InventoryItemsSlots;

    [SerializeField] public string inventoryName = string.Empty;
    [SerializeField] public string saveFilePath = string.Empty;



    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        _inventory.SetActive(false);
    }

    public void Update()
    {

        if (Input.GetKeyDown(KeyCode.F1))
            Save(); 

        if (Input.GetKeyDown(KeyCode.F2))
            Load();  

    }

    // Add item to List
    public bool AddItem(Item item)
    {
        bool itemExists = DoesItemExist(item);
        if (item != null)
        {
            if (itemExists)
            {
                item.Quantity++;
                Debug.Log(item.ItemName + "quantity is" + item.Quantity);
                return true;
            }
            if (itemExists == false && _itemList.Count < _MaxInventorySlots)
            {
                Debug.Log("Added " + item.ItemName);
                _itemList.Add(item);
                return true;
            }
            else
            {
                Debug.Log("Max Stack Size reached. Cannot add more.");
                return false;
            }
        }
        return false;
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
                                                                        // when close inventory and play again, doesnt reset the quantity
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
                InventoryItemsSlots[i].AddItem(_itemList[i]);
            
        }
    }

    // Checks for the item in the inventory
    public bool DoesItemExist(Item item)
    {
        // Finds if the item already exists in the inventory
        List<Item> existingItems = _itemList.FindAll(delegate (Item s) { return s == item; });

        // If the item exists or is found above then find any that has < MaxStackSize
        if (existingItems.Count > 0)
        {
            Item itemFound = existingItems.Find(delegate (Item s) { return s.Quantity < item.MaxStackSize; });
            if (itemFound != null)
            {
                return true;
            }
        }
        return false;
    }


    public bool Save()
    {
        try
        {
            saveFilePath = Application.dataPath + $"/{inventoryName}.json";

            string dataFile = JsonUtility.ToJson(this, true);
            File.WriteAllText(saveFilePath, dataFile);

            Debug.Log("Saved.");
            return true;
        }
        catch (Exception)
        {
            Debug.Log("Error saving.");
            return false;
        }

    }

    public bool Load()
    {
        try
        {
            saveFilePath = Application.dataPath + $"/{inventoryName}.json";
            if (File.Exists(saveFilePath))
            {

                string dataFile = File.ReadAllText(saveFilePath);

                JsonUtility.FromJsonOverwrite(dataFile, this);

                Debug.Log("Loaded.");
            }
            return true;
        }
        catch (Exception)
        {
            Debug.Log("Unable to load data.");
            return false;

        }

    }

}
