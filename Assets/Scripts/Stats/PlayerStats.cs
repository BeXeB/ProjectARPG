
public class PlayerStats : CharacterStats
{
    private void Start()
    {
        Equipment.instance.onEquipmentChangedCallback += OnEquipmentChanged;
    }

    void OnEquipmentChanged(Equipable newItem, Equipable oldItem)
    {
        if (newItem != null)
        {
            intelligence.AddModifier(newItem.intelligenceModifier);
            strength.AddModifier(newItem.strengthModifier);
            dexterity.AddModifier(newItem.dexterityModifier);
            vitality.AddModifier(newItem.vitalityModifier);
            if (newItem is Weapon)
            {
                damage.AddModifier(((Weapon)newItem).damageModifier);
            }
            else if (newItem is Armor)
            {
                armor.AddModifier(((Armor)newItem).armorModifier);
            }
        }

        if (oldItem != null)
        {
            intelligence.RemoveModifier(newItem.intelligenceModifier);
            strength.RemoveModifier(newItem.strengthModifier);
            dexterity.RemoveModifier(newItem.dexterityModifier);
            vitality.RemoveModifier(newItem.vitalityModifier);
            if (newItem is Weapon)
            {
                damage.RemoveModifier(((Weapon)newItem).damageModifier);
            }
            else if (newItem is Armor)
            {
                armor.RemoveModifier(((Armor)newItem).armorModifier);
            }
        }
    }
}
