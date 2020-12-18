using UnityEngine;

public class ItemPickup : Interactable
{
    [SerializeField] public Item item;

    public override void OnInteract()
    {
        base.OnInteract();
        PickUp();
    }

    private void PickUp()
    {
        if (Inventory.instance.AddItem(item))
        {
            Destroy(gameObject);
        }
    }
}
