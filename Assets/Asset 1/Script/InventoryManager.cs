using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance;

    public List<ItemData> inventory = new List<ItemData>();

    public InventoryUI InventoryUI;

    private void Awake()
    {
        instance = this;
    }

    public void AddItem(ItemData item)
    {
        inventory.Add(item);

        InventoryUI.UpdateUI();
    }

}