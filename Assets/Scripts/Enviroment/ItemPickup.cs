using UnityEngine;

public class ItemPickup : Interactable
{
    [SerializeField] Item item;

    public override void OnInteract()
    {
        base.OnInteract();
        PickUp();
    }

    private void PickUp()
    {
        print("picked up the item: " + item.name);
        if (Inventory.instance.AddItem(item))
        {
            Destroy(gameObject);
        }
    }
}
