using UnityEngine;

[CreateAssetMenu(fileName = "New Armor", menuName = "Inventory/Armor")]
public class Armor : Equipable
{
    public int armorModifier = 0;
    public int baseArmor = 0;
}
