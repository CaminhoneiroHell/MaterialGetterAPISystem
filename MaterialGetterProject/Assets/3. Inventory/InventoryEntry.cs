using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryEntry 
{
    //public ItemPickUp invEntry;
    public int stackSize;
    public int inventorySlot;
    public int hotbarSlot;
    public Sprite sprite;

    public InventoryEntry(int stackSize, /*ItemPickUp invEntry,*/ Sprite sprite)
    {
        //this.invEntry = invEntry;

        this.stackSize = stackSize;
        this.hotbarSlot = 0;
        this.inventorySlot = 0;
        this.sprite = sprite;
    }

}
