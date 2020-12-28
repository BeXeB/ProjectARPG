using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] Transform itemsParent;
    Inventory inventory;
    InventorySlot[] slots;

    private void Start()
    {
        inventory = PlayerManager.instance.player.GetComponent<Inventory>();
        inventory.onItemChangedCallback += UpdateUI;
        slots = itemsParent.GetComponentsInChildren<InventorySlot>();
    }

    void UpdateUI()
    {
        var items = inventory.GetItems();
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < items.Count)
            {
                slots[i].AddItem(items[i]);
            }
            else
            {
                slots[i].CleatSlot();
            }
        }
    }
}
