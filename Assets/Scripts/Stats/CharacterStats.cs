using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    [SerializeField] protected float armorPotency = 100f; // how much armor to 50% damage redu
    [SerializeField] protected float strengthPotency = 100f; //  how much to double damage
    [SerializeField] protected float inteligencePotency = 100f; // how much to double spell damage
    [SerializeField] protected float vitalityPotency = 10f; // hp / vit

    [SerializeField] protected Stat intelligence;
    [SerializeField] protected Stat strength;
    [SerializeField] protected Stat vitality;
    [SerializeField] protected Stat dexterity;
    [SerializeField] protected Stat armor;
    [SerializeField] protected Stat damage;
    protected bool died = false;

    protected float currentMaxHealth = 0;
    protected float baseMaxHealth = 100;
    protected float currentHealt { get; private set; }

    private void Awake()
    {
        currentHealt = baseMaxHealth;
        currentHealt = currentMaxHealth;
    }

    public void TakeDamage(float incDmg)
    {
        //reduce dmg with armor
        //NewDmg = Dmg/(1+(Armor/100))
        float reducedIncDmg = incDmg / (1 + (armor.GetValue() / armorPotency));
        currentHealt -= reducedIncDmg;

        if (currentHealt <= 0)
        {
            Die();
        }
    }

    public float CalcWeaponDmg(float baseWeaponDamage)
    {
        float dmg = damage.GetValue() + baseWeaponDamage;
        dmg *= 1 + (strength.GetValue() / strengthPotency);
        return dmg;
    }

    public float CalcSkillDmg(float baseSpellDamage)
    {
        float dmg = damage.GetValue() + baseSpellDamage;
        dmg *= 1 + (intelligence.GetValue() / inteligencePotency);
        return dmg;
    }

    public void Heal(float ammount)
    {
        currentHealt += ammount;
        Mathf.Clamp(currentHealt, 0, currentMaxHealth);
    }

    public virtual void Die()
    {
        died = true;
    }
}
