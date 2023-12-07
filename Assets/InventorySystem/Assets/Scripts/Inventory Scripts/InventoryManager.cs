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

    [HideInInspector]
    //where picked-up items will be stored
    [SerializeField] public List<Item> _itemList = new List<Item>();

    [HideInInspector]
    //where the inventory slots holding the picked-up items will be stored
    [SerializeField] public List<InventorySlot> _inventoryItems = new List<InventorySlot>();

    //where the list of category tabs in the UI will be stored
    [SerializeField] public List<InventoryCategory> itemCategories = new List<InventoryCategory>();

    [SerializeField] public int _MaxInventorySlots = 2;  

    public GameObject _inventory;        //Master gameobject where all your Inventory is stored
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
        if (item != null)
        {
            InventorySlot itemExists = DoesItemExist(item);

            if (itemExists != null)
            {
                itemExists.quantity++;
                Debug.Log(item.ItemName + " quantity is " + item.Quantity);
                return true;
            }
            if (itemExists == null && _inventoryItems.Count < _MaxInventorySlots)
            {
                Debug.Log("Added " + item.ItemName);
                InventorySlot slot = new InventorySlot(item.ID, item);

                _inventoryItems.Add(slot);
                return true;
            }
            
            else
            {
                Debug.Log("Max Inventory Size reached. Cannot add more.");
                return false;
            }
        }
        return false;
    }

    // remove item from list
    public void RemoveItem(Item item)
    {
        InventorySlot slot = _inventoryItems.Find(delegate (InventorySlot s) { return s.slotItem == item; });
        if(slot != null)
        {
            _inventoryItems.Remove(slot);
        }
        
    }

    public void ListItems()
    {
        ListItems(_inventoryItems);
    }

    // Finds and changes the name, item icon, and remove button for each Item picked up. Displays the quantity in the UI.
    public void ListItems(List<InventorySlot> invItems)
    {
        foreach (var item in invItems)
        {
            GameObject obj = Instantiate(_inventoryItem, _itemContent);
            var itemName = obj.transform.Find("ItemName").GetComponent<TMP_Text>();
            var itemIcon = obj.transform.Find("ItemImage").GetComponent<Image>();
            var removeButton = obj.transform.Find("RemoveItemButton").GetComponent<Button>();
            var itemQuantityDisplay = obj.transform.Find("ItemQuantity").GetComponent<TMP_Text>();

            itemName.text = item.slotItem.ItemName;
            itemIcon.sprite = item.slotItem.Icon;

            
            itemQuantityDisplay.text = item.quantity.ToString();

            if(_enableRemove.isOn)
            {
                removeButton.gameObject.SetActive(true);
            }
        }

        SetInventoryItems(invItems);
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
    public void SetInventoryItems(List<InventorySlot> invItems)
    {
        InventoryItemsSlots = _itemContent.GetComponentsInChildren<InventoryItemController>();

        for (int i = 0; i < invItems.Count; i++)
        {      
                InventoryItemsSlots[i].AddItem(invItems[i].slotItem);
            
        }
    }

    // Checks for the item in the inventory
    public InventorySlot DoesItemExist(Item item)
    {
        // Finds if the item already exists in the inventory
        List<InventorySlot> existingItems = _inventoryItems.FindAll(delegate (InventorySlot s) { return s.slotItem == item; });

        // If the item exists or is found above then find any that has < MaxStackSize
        if (existingItems.Count > 0)
        {
            InventorySlot itemFound = existingItems.Find(delegate (InventorySlot s) { return s.quantity < item.MaxStackSize; });
            if (itemFound != null)
            {
                return itemFound;
            }
        }
        return null;
    }

    //Filters items based on category
    public void CategoryFilter(string category)
    {
        CleanList();
        List<InventorySlot> filterList = _inventoryItems.FindAll(delegate (InventorySlot s) { return s.slotItem.Category.Name == category; });
        ListItems(filterList);
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
