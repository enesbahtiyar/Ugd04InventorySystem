
using UnityEngine;

public class ItemPickUp : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Item item = collision.GetComponent<Item>();

        if(item != null)
        {
            InventoryManager.Instance.AddItem(InventoryLocation.player, item, collision.gameObject);
        }
    }
}
