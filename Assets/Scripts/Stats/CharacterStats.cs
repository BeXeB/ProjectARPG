using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public delegate void OnHealthChanged(GameObject go, float currentHealt, float maxHealth);
    public static OnHealthChanged onHelathChangedCallback;
    public delegate void OnDied(GameObject go);
    public static OnDied onDiedCallback;

    [SerializeField] protected Stat armorPotency;// how much armor to 50% damage redu
    [SerializeField] protected Stat strengthPotency; //  how much to double damage
    [SerializeField] protected Stat inteligencePotency; // how much to double spell damage
    [SerializeField] protected Stat vitalityPotency; // hp / vit
    [SerializeField] protected Stat dexterityPotency; //how much dex to 50% cdr
    [SerializeField] protected Stat moveSpeed;
    [SerializeField] protected Stat maxHealth;
    [SerializeField] protected Stat intelligence;
    [SerializeField] protected Stat strength;
    [SerializeField] protected Stat vitality;
    [SerializeField] protected Stat dexterity;
    [SerializeField] protected Stat armor;
    [SerializeField] protected Stat damage;
    [SerializeField] protected Stat damageReductionPercentage;
    [SerializeField] protected Stat critChance;
    [SerializeField] protected Stat critDamage;
    [SerializeField] protected Stat spellDamagePercentage;
    protected bool died = false;
    protected bool spellCrit = false;
    public float currentHealt { get; protected set; }

    #region getters/setters

    public Stat GetIntPot()
    {
        return inteligencePotency;
    }

    public Stat GetDexPot()
    {
        return dexterityPotency;
    }

    public Stat GetSpellDamagePercentage()
    {
        return spellDamagePercentage;
    }

    public Stat GetIntelligence()
    {
        return intelligence;
    }

    public Stat GetDexterity()
    {
        return dexterity;
    }

    public Stat GetArmorPot()
    {
        return armorPotency;
    }

    public Stat GetVitPot()
    {
        return vitalityPotency;
    }

    public Stat GetStrPot()
    {
        return strengthPotency;
    }

    public Stat GetVitality()
    {
        return vitality;
    }

    public Stat GetArmor()
    {
        return armor;
    }

    public Stat GetMaxHealth()
    {
        return maxHealth;
    }

    public Stat GetStrength()
    {
        return strength;
    }

    public Stat GetMovementSpeed()
    {
        return moveSpeed;
    }

    public Stat GetDamage()
    {
        return damage;
    }

    public Stat GetCritChance()
    {
        return critChance;
    }

    public Stat GetCritDamage()
    {
        return critDamage;
    }

    public bool GetDied()
    {
        return died;
    }

    public void SetDied(bool value)
    {
        died = value;
    }

    public void SetSpellCrit(bool value)
    {
        spellCrit = value;
    }

    #endregion

    private void Awake()
    {
        currentHealt = maxHealth.GetValue();
    }

    public virtual void TakeDamage(float incDmg)
    {
        if (!died)
        {
            float reducedIncDmg = incDmg * (1 + (damageReductionPercentage.GetValue() / 100));
            reducedIncDmg /= (1 + (armor.GetValue() / armorPotency.GetValue()));

            currentHealt -= reducedIncDmg;
            currentHealt = Mathf.Clamp(currentHealt, 0, maxHealth.GetValue());

            onHelathChangedCallback?.Invoke(this.gameObject, currentHealt, maxHealth.GetValue());

            if (currentHealt == 0)
            {
                Die();
            }
        }
    }

    public float CalcWeaponDmg(float baseWeaponDamage)
    {
        float dmg = (damage.GetValue() + baseWeaponDamage);
        dmg *= (1 + (strength.GetValue() / strengthPotency.GetValue()));
        dmg = CalcCrit(dmg);
        return dmg;
    }

    private float CalcCrit(float damage)
    {
        float rnd = Random.Range(0f, 100f);
        if (rnd <= critChance.GetValue())
        {
            damage *= (critDamage.GetValue() / 100f);
        }
        return damage;
    }

    public float CalcSkillDmg(float baseSpellDamage)
    {
        float dmg = damage.GetValue() + baseSpellDamage;
        dmg *= (1 + spellDamagePercentage.GetValue());
        dmg *= (1 + (intelligence.GetValue() / inteligencePotency.GetValue()));
        if (spellCrit)
        {
            dmg = CalcCrit(dmg);
        }
        return dmg;
    }

    public void Heal(float ammount)
    {
        currentHealt += ammount;
        Mathf.Clamp(currentHealt, 0f, maxHealth.GetValue());
        onHelathChangedCallback?.Invoke(this.gameObject, currentHealt, maxHealth.GetValue());
    }

    public void Execute()
    {
        if (!died)
        {
            Die();
        }
    }

    public virtual void Die()
    {
        died = true;
        onDiedCallback?.Invoke(gameObject);
    }
}
