using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using OGPC;

public class InventoryDisplay : MonoBehaviour
{

    public RawImage[] inventoryImages;
    public TextMeshProUGUI[] inventoryCount;

    public GameObject slotPrefab;

    private Inventory inventory;

    private bool invEnabled = false;

    private void Awake()
    {
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
        inventory.SetDisplay(this);
        inventoryImages = new RawImage[inventory.inventorySize];
        inventoryCount = new TextMeshProUGUI[inventory.inventorySize];
        GameObject[] inventoryTemp = new GameObject[inventory.inventorySize];
        for (int i = 0; i < inventoryTemp.Length; i++)
        {
            inventoryTemp[i] = Instantiate(slotPrefab, transform);

            inventoryImages[i] = inventoryTemp[i].transform.GetChild(0).GetComponent<RawImage>();
            inventoryCount[i] = inventoryTemp[i].transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        }
    }

    public void InventoryChanged()
    {
        for (int i = 0; i < inventory.inventory.Length; i++)
        {
            if (inventory.inventory[i] == null)
            {
                inventoryImages[i].gameObject.SetActive(false);
                inventoryCount[i].text = "";
                inventoryCount[i].gameObject.SetActive(false);

                continue;
            }
            if (!inventory.inventory[i].isEmpty())
            {
                inventoryImages[i].texture = inventory.inventory[i].item.m_Sprites[0].texture;
                inventoryImages[i].gameObject.SetActive(true);
                inventoryCount[i].text = inventory.inventory[i].stackedAmount.ToString();
                inventoryCount[i].gameObject.SetActive(true);
            }
        }
    }
}
