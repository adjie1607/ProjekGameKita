using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public Transform slotParent;
    public GameObject slotPrefab;

    public void UpdateUI()
    {
        if (slotParent == null)
        {
            Debug.LogError("Slot Parent belum diisi!");
            return;
        }

        if (slotPrefab == null)
        {
            Debug.LogError("Slot Prefab belum diisi!");
            return;
        }

        foreach (Transform child in slotParent)
        {
            Destroy(child.gameObject);
        }

        foreach (ItemData item in InventoryManager.instance.inventory)
        {
            GameObject slot = Instantiate(slotPrefab, slotParent);

            slot.GetComponent<InventorySlot>().SetItem(item);
        }
    }
}