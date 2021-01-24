public class PlayerStats : CharacterStats
{
    private LevelSystem levelSystem;
    public float healthGrowthMultiplyer = 1.5f;
    public int inteligenceGrowth = 1;
    public int strengthGrowth = 1;
    public int vitalityGrowth = 1;
    public int dexterityGrowth = 1;
    public int armorGrowth = 1;
    public int damageGrowth = 1;

    private void Start()
    {
        levelSystem = PlayerManager.instance.player.GetComponent<LevelSystem>();
        levelSystem.onLevelChangedCallback += OnLevelUp;
        PlayerManager.instance.player.GetComponent<EquipmentController>().onEquipmentChangedCallback += OnEquipmentChanged;
    }

    void OnLevelUp()
    {
        //grow stats and hp
        maxHealth.SetBaseValue(maxHealth.GetBaseValue() * healthGrowthMultiplyer);
        maxHealth.RemoveModifier(vitality.GetValue() * vitalityPotency.GetValue());
        intelligence.SetBaseValue(intelligence.GetBaseValue() + inteligenceGrowth);
        strength.SetBaseValue(strength.GetBaseValue() + strengthGrowth);
        vitality.SetBaseValue(vitality.GetBaseValue() + vitalityGrowth);
        dexterity.SetBaseValue(dexterity.GetBaseValue() + dexterityGrowth);
        armor.SetBaseValue(armor.GetBaseValue() + armorGrowth);
        damage.SetBaseValue(damage.GetBaseValue() + damageGrowth);
        maxHealth.AddModifier(vitality.GetValue() * vitalityPotency.GetValue());
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
            intelligence.RemoveModifier(oldItem.intelligenceModifier);
            strength.RemoveModifier(oldItem.strengthModifier);
            dexterity.RemoveModifier(oldItem.dexterityModifier);
            vitality.RemoveModifier(oldItem.vitalityModifier);
            if (oldItem is Weapon)
            {
                damage.RemoveModifier(((Weapon)oldItem).damageModifier);
            }
            else if (oldItem is Armor)
            {
                armor.RemoveModifier(((Armor)oldItem).armorModifier);
            }
        }
    }
}
