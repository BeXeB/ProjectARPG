using UnityEngine;

public class EquipmentController : MonoBehaviour
{
    Equipable[] currentEquipment;
    InventoryController inventory;

    public delegate void OnEquipmentChanged(Equipable newItem, Equipable oldItem);
    public OnEquipmentChanged onEquipmentChangedCallback;

    [SerializeField] private Transform weaponParent;

    private void Start()
    {
        inventory = GetComponent<InventoryController>();
        int numberOfslots = System.Enum.GetNames(typeof(EquipmentSlot)).Length;
        currentEquipment = new Equipable[numberOfslots];
    }

    public void Equip(Equipable newItem)
    {
        int slotIndex = (int)newItem.equipSlot;
        Equipable oldItem = Unequip(slotIndex);
        currentEquipment[slotIndex] = newItem;
        if (newItem.equipSlot == EquipmentSlot.Weapon)
        {
            if (oldItem != null)
            {
                Destroy(weaponParent.GetChild(1).gameObject); //Destroy weapon model
            }
            var newObject = Instantiate(((Weapon)newItem).weaponModel, weaponParent.position, weaponParent.rotation, weaponParent);
            newObject.transform.localScale /= 100;
        }
        if (onEquipmentChangedCallback != null)
        {
            onEquipmentChangedCallback.Invoke(newItem, oldItem);
        }
    }

    public Equipable Unequip(int slotIndex)
    {
        Equipable oldItem = null;
        if (currentEquipment[slotIndex] != null)
        {
            oldItem = currentEquipment[slotIndex];
            inventory.AddItem(oldItem);
            currentEquipment[slotIndex] = null;
        }
        if (onEquipmentChangedCallback != null)
        {
            onEquipmentChangedCallback.Invoke(null, oldItem);
        }
        return oldItem;
    }

    public Equipable[] GetEquipment()
    {
        return currentEquipment;
    }
}
