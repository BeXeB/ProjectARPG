using UnityEngine;
using System.Collections;

public class PlayerStats : CharacterStats
{
    public delegate void OnShieldChanged(float currentShield);
    public OnShieldChanged onShieldChangedCallback;
    public delegate void OnShieldBreak();
    public static OnShieldBreak onShieldBreakCallback;
    [SerializeField] protected Stat maxShield;
    public float currentShield { get; protected set; }
    protected Core core;
    private LevelSystem levelSystem;
    private float healthGrowthMultiplyer = 1.5f;
    private int inteligenceGrowth = 1;
    private int strengthGrowth = 1;
    private int vitalityGrowth = 1;
    private int dexterityGrowth = 1;
    private int armorGrowth = 1;
    private int damageGrowth = 1;

    #region Getters/Setters

    public Core GetCurrentCore()
    {
        return core;
    }

    public Stat GetShield()
    {
        return maxShield;
    }

    #endregion

    private void Start()
    {
        levelSystem = PlayerManager.instance.player.GetComponent<LevelSystem>();
        levelSystem.onLevelChangedCallback += OnLevelUp;
        PlayerManager.instance.player.GetComponent<EquipmentController>().onEquipmentChangedCallback += OnEquipmentChanged;
        maxShield.onStatChangeCallback += OnShieldChange;
    }

    public override void TakeDamage(float incDmg)
    {
        if (!died)
        {
            float reducedIncDmg = incDmg * (1 + (damageReductionPercentage.GetValue() / 100));
            reducedIncDmg /= (1 + (armor.GetValue() / armorPotency.GetValue()));

            if (currentShield > 0 && core)
            {
                StopAllCoroutines();
                currentShield -= reducedIncDmg;
                currentShield = Mathf.Clamp(currentShield, 0, maxShield.GetValue());
                onShieldChangedCallback?.Invoke(currentShield);
                if (currentShield == 0)
                {
                    onShieldBreakCallback?.Invoke();
                }
                StartCoroutine(ShieldRechargeBehaviour());
            }
            else
            {
                currentHealt -= reducedIncDmg;
                currentHealt = Mathf.Clamp(currentHealt, 0, maxHealth.GetValue());
                onHelathChangedCallback?.Invoke(this.gameObject, currentHealt, maxHealth.GetValue());
                if (currentHealt == 0)
                {
                    Die();
                }
            }
        }
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
            maxHealth.RemoveModifier(vitality.GetValue() * vitalityPotency.GetValue());
            vitality.AddModifier(newItem.vitalityModifier);
            maxHealth.AddModifier(vitality.GetValue() * vitalityPotency.GetValue());
            if (newItem is Weapon)
            {
                damage.AddModifier(((Weapon)newItem).damageModifier);
            }
            else if (newItem is Armor)
            {
                armor.AddModifier(((Armor)newItem).armorModifier);

            }
            else if (newItem is Core)
            {
                core = ((Core)newItem);
                maxShield.SetBaseValue(core.shield);
            }
        }

        if (oldItem != null)
        {
            intelligence.RemoveModifier(oldItem.intelligenceModifier);
            strength.RemoveModifier(oldItem.strengthModifier);
            dexterity.RemoveModifier(oldItem.dexterityModifier);
            maxHealth.RemoveModifier(vitality.GetValue() * vitalityPotency.GetValue());
            vitality.RemoveModifier(oldItem.vitalityModifier);
            maxHealth.AddModifier(vitality.GetValue() * vitalityPotency.GetValue());
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

    private void OnShieldChange(Stat shield)
    {
        currentShield = shield.GetValue();
        onShieldChangedCallback?.Invoke(currentShield);
    }

    IEnumerator ShieldRechargeBehaviour()
    {
        yield return new WaitForSeconds(core.rechargeDelay);
        while (currentShield < maxShield.GetValue())
        {
            currentShield += core.rechargeRate * Time.deltaTime;
            currentShield = Mathf.Clamp(currentShield, 0f, maxShield.GetValue());
            onShieldChangedCallback?.Invoke(currentShield);
            yield return null;
        }
        yield return null;
    }

    public void RefillShield()
    {
        if (core)
        {
            currentShield = maxShield.GetValue();
        }
    }
}
