using UnityEngine;

public class EquipmentUI : MonoBehaviour
{

    [SerializeField] Transform equipmentParent;

    EquipmentController equipment;

    EquipmentUISlot[] slots;

    private void Start()
    {
        equipment = PlayerManager.instance.player.GetComponent<EquipmentController>();
        equipment.onEquipmentChangedCallback += UpdateUI;
        slots = equipmentParent.GetComponentsInChildren<EquipmentUISlot>();
    }

    void UpdateUI(Equipable newItem, Equipable oldItem)
    {
        var currentEquipment = equipment.GetEquipment();
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < currentEquipment.Length && currentEquipment[i] != null)
            {
                slots[i].AddEquipment(currentEquipment[i]);
            }
            else
            {
                slots[i].CleatSlot();
            }
        }
    }
}
