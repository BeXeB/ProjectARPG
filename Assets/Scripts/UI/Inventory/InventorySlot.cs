using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    [SerializeField] Image icon;
    [SerializeField] Button removeButton;
    Item item;

    public void AddItem(Item item)
    {
        this.item = item;
        icon.sprite = item.icon;
        icon.enabled = true;
        removeButton.interactable = true;
    }

    public void CleatSlot()
    {
        item = null;
        icon.sprite = null;
        icon.enabled = false;
        removeButton.interactable = false;
    }

    public void OnRemoveButton()
    {
        PlayerManager.instance.player.GetComponent<Inventory>().RemoveItem(item);
    }

    public void UseItem()
    {
        if (item != null)
        {
            item.Use();
        }
    }
}
