using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public ItemData itemData;
    public AudioClip pickupSound;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            InventoryManager.instance.AddItem(itemData);

            AudioManager.instance.PlaySFX(pickupSound);

            Destroy(gameObject);
        }
    }
}