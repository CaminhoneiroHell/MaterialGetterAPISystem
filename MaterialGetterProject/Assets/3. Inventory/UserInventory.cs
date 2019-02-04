using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UserInventory : MonoBehaviour
{
    public static UserInventory instance;

    public Image[] hotBarDisplayHolders = new Image[4];
    public GameObject InventoryDisplayHolder;
    public Image[] inventoryDisplaySlots = new Image[4];

    int idCount = 1;
    bool addedItem = true;

    public Dictionary<int, InventoryEntry> itensInventory = new Dictionary<int, InventoryEntry>(); // Storage of inventory
    public InventoryEntry itemEntry;


    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        itemEntry = new InventoryEntry(0, /*null,*/ null);
        itensInventory.Clear();

        inventoryDisplaySlots = InventoryDisplayHolder.GetComponentsInChildren<Image>();
    }

    //public void StoreItem(ItemPickup itemPickup)
    //{

    //}

    // Update is called once per frame
    void Update()
    {
        
    }
}
