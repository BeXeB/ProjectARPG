using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public delegate void OnDamageTaken(GameObject go, float currentHealt, float maxHealth);
    public static OnDamageTaken onDamageTakenCallback;

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
    [SerializeField] protected Stat baseDamageIncreasePrecentage;
    [SerializeField] protected Stat damageReduPercentage;
    [SerializeField] protected Stat critChance;
    [SerializeField] protected Stat critDamage;

    protected bool died = false;
    protected float currentHealt { get; private set; }

    #region getters/setters

    public Stat GetStrPot()
    {
        return strengthPotency;
    }

    public Stat GetStrength()
    {
        return strength;
    }

    public Stat GetMovementSpeed()
    {
        return moveSpeed;
    }

    public Stat GetDamageIncreasePercentage()
    {
        return baseDamageIncreasePrecentage;
    }

    public Stat GetCritChance()
    {
        return critChance;
    }

    public Stat GetCritDamage()
    {
        return critDamage;
    }

    #endregion

    private void Awake()
    {
        currentHealt = maxHealth.GetValue();
    }

    public void TakeDamage(float incDmg)
    {
        float reducedIncDmg = incDmg * (damageReduPercentage.GetValue() / 100);
        reducedIncDmg = reducedIncDmg / (1 + (armor.GetValue() / armorPotency.GetValue()));
        currentHealt -= reducedIncDmg;

        onDamageTakenCallback.Invoke(this.gameObject, currentHealt, maxHealth.GetValue());

        if (currentHealt <= 0)
        {
            Die();
        }
    }

    public float CalcWeaponDmg(float baseWeaponDamage)
    {
        float dmg = (damage.GetValue() + baseWeaponDamage) * (baseDamageIncreasePrecentage.GetValue() / 100);
        dmg *= 1 + (strength.GetValue() / strengthPotency.GetValue());
        dmg = CalcCrit(dmg);
        return dmg;
    }

    private float CalcCrit(float damage)
    {
        float rnd = Random.Range(0f, 100f);
        if (rnd <= critChance.GetValue())
        {
            damage *= (critDamage.GetValue() / 100);
        }
        return damage;
    }

    public float CalcSkillDmg(float baseSpellDamage)
    {
        float dmg = damage.GetValue() + baseSpellDamage;
        dmg *= 1 + (intelligence.GetValue() / inteligencePotency.GetValue());
        return dmg;
    }

    public void Heal(float ammount)
    {
        currentHealt += ammount;
        Mathf.Clamp(currentHealt, 0f, maxHealth.GetValue());
    }
    public virtual void Die()
    {
        died = true;
    }
}
