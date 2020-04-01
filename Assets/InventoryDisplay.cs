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
    private Inventory inventory;

    private bool invEnabled = false;

    private void Awake()
    {
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
        inventoryImages = new RawImage[32];
        inventoryCount = new TextMeshProUGUI[32];
        GameObject[] inventoryTemp = GameObject.FindGameObjectsWithTag("Item Slot");
        for (int i = 0; i < inventoryTemp.Length; i++)
        {
            inventoryImages[i] = inventoryTemp[i].transform.GetChild(0).GetComponent<RawImage>();
            inventoryCount[i] = inventoryTemp[i].transform.GetChild(1).GetComponent<TextMeshProUGUI>();

            inventoryCount[i].gameObject.SetActive(false);
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < inventory.inventory.Length; i++)
        {
            if (inventory.inventory[i] == null) continue;
            if (!inventory.inventory[i].isEmpty())
            {
                inventoryImages[i].texture = inventory.inventory[i].item.m_Sprites[0].texture;
                inventoryCount[i].text = inventory.inventory[i].stackedAmount.ToString();
                if (int.Parse(inventoryCount[i].text) > 0) inventoryCount[i].gameObject.SetActive(true);
                else inventoryCount[i].gameObject.SetActive(false);
            }
        }
    }
}
