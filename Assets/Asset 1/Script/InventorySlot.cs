using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Image icon;

    public void SetItem(ItemData item)
    {
        icon.sprite = item.icon;
        icon.enabled = true;
    }
}