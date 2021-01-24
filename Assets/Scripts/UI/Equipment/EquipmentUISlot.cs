using UnityEngine;
using UnityEngine.UI;

public class EquipmentUISlot : MonoBehaviour
{
    [SerializeField] Image icon;
    Equipable equipment;

    public void AddEquipment(Equipable equipment)
    {
        this.equipment = equipment;
        icon.sprite = equipment.icon;
        icon.enabled = true;
    }

    public void CleatSlot()
    {
        equipment = null;
        icon.sprite = null;
        icon.enabled = false;
    }

    public void Unequip()
    {
        PlayerManager.instance.player.GetComponent<EquipmentController>().Unequip((int)equipment.equipSlot);
    }
}
