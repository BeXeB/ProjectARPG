using UnityEngine;

public class Item : MonoBehaviour
{
    new public string name = "New Item";
    public Sprite icon = null;
    public bool isDefaultItem = false;
    public virtual void Use() { }

    public void RemoveFromInventory()
    {
        PlayerManager.instance.player.GetComponent<Inventory>().RemoveItem(this);
    }
}
