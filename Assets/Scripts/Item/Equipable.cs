using UnityEngine;

public class Equipable : Item
{
    public EquipmentSlot equipSlot;

    public int level = 0;
    public int intelligenceModifier = 0;
    public int strengthModifier = 0;
    public int vitalityModifier = 0;
    public int dexterityModifier = 0;

    public override void Use()
    {
        base.Use();
        PlayerManager.instance.player.GetComponent<Equipment>().Equip(this);
        RemoveFromInventory();
    }
}

public enum EquipmentSlot { Head, Shoulders, Arms, Torso, Legs, Feet, Core, AI_Module, Weapon, Off_Hand }