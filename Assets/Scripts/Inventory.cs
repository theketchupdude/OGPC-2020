using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OGPC;

public class Inventory : MonoBehaviour
{

    public int inventorySize = 32;
    public ItemContainer[] inventory;

    public Item debugItem;

    // Start is called before the first frame update
    void Start()
    {
        inventory = new ItemContainer[inventorySize];
    }

    private void Update()
    {
        // if (Input.GetKeyDown(KeyCode.Space))
        // {
        //     AddItem(debugItem, 1); //***** These fields can be replaced with item.blank instead of hardcoded values
        // }
        //else if (Input.GetKeyDown(KeyCode.B))
        //{
            //RemoveItem("Dirt"); //***** Same here
        //}
    }

    public void AddItem(string itemName, int amount)
    {
        foreach (ItemContainer item in inventory)
        {
            if (item == null) continue;
            if (item.isEmpty()) continue; // if item slot is empty then we can ignore it and continue to next slot
            if (!item.matches(itemName)) continue; // if item slot doesn't match then we can ignore it and continue to next slot
            if (item.stackedAmount >= item.item.maxStackSize) continue; // test if the slot is full, if it is another might not be so continue to next slot

            item.stackedAmount++;
            return; // item has been handled and placed in inventory
        }

        for (int i = 0; i < inventory.Length; i++) // either item wasn't found in inventory already or other slots were full
        {
            if (inventory[i] == null || inventory[i].isEmpty())
            {
                inventory[i] = new ItemContainer(itemName, amount);
                print("Item added to new slot");
                return; // Item was added to inventory in a new slow
            }
        }
    }

    public void AddItem(Item itm, int amount)
    {
        foreach (ItemContainer item in inventory)
        {
            if (item == null) continue;
            if (item.isEmpty()) continue; // if item slot is empty then we can ignore it and continue to next slot
            if (!item.matches(itm)) continue; // if item slot doesn't match then we can ignore it and continue to next slot
            if (item.stackedAmount >= itm.maxStackSize) continue; // test if the slot is full, if it is another might not be so continue to next slot

            item.stackedAmount++;
            return; // item has been handled and placed in inventory
        }

        for (int i = 0; i < inventory.Length; i++) // either item wasn't found in inventory already or other slots were full
        {
            if (inventory[i] == null || inventory[i].isEmpty())
            {
                inventory[i] = new ItemContainer(itm, amount);
                print("Item added to new slot");
                return; // Item was added to inventory in a new slow
            }
        }
    }

    public void RemoveItem(string itemName)
    {
        foreach (ItemContainer item in inventory)
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
