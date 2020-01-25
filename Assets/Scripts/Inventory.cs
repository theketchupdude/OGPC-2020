using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OGPC;

public class Inventory : MonoBehaviour
{

    public int inventorySize = 32;
    public InventoryItem[] inventory;

    // Start is called before the first frame update
    void Start()
    {
        inventory = new InventoryItem[inventorySize];
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            AddItemToInventory("Dirt", 4, 1); //***** These fields can be replaced with item.blank instead of hardcoded values
        }
        else if (Input.GetKeyDown(KeyCode.B))
        {
            RemoveItemFromInventory("Dirt"); //***** Same here
        }
    }

    public void AddItemToInventory(string itemName, int maxStackSize, int amount)
    {
        foreach (InventoryItem item in inventory)
        {
            if (item.isEmpty()) continue; // if item slot is empty then we can ignore it and continue to next slot
            if (!item.matches(itemName)) continue; // if item slot doesn't match then we can ignore it and continue to next slot
            if (item.stackedAmount >= maxStackSize) continue; // test if the slot is full, if it is another might not be so continue to next slot

            item.stackedAmount++;
            return; // item has been handled and placed in inventory
        }

        for (int i = 0; i < inventory.Length; i++) // either item wasn't found in inventory already or other slots were full
        {
            if (inventory[i].isEmpty())
            {
                inventory[i] = new InventoryItem(itemName, maxStackSize, amount);
                print("Item added to new slot");
                return; // Item was added to inventory in a new slow
            }
        }
    }

    public void RemoveItemFromInventory(string itemName)
    {
        foreach (InventoryItem item in inventory)
        {
            if (item.matches(itemName)) // item was found in inventory
            {
                if (item.stackedAmount-- == 1) // slot is out of items
                {
                    item.clearSlot(); // remove item from slot
                }
                return;
            }
        }
    }
}
