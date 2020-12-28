using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    [SerializeField] private float armorPotency = 100f;
    [SerializeField] private float strengthPotency = 100f;
    [SerializeField] private float inteligencePotency = 100f;
    [SerializeField] protected Stat intelligence;
    [SerializeField] protected Stat strength;
    [SerializeField] protected Stat vitality;
    [SerializeField] protected Stat dexterity;
    [SerializeField] protected Stat armor;
    [SerializeField] protected Stat damage;
    protected bool died = false;

    protected float maxHealth = 100;
    protected float currentHealt { get; private set; }

    private void Awake()
    {
        currentHealt = maxHealth;
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

    public float CalcWeaponDmg()
    {
        float dmg = damage.GetValue();
        dmg *= 1 + (strength.GetValue() / strengthPotency);
        return dmg;
    }

    public float CalcSpellDmG(float baseSpellDamage)
    {
        baseSpellDamage *= 1 + (intelligence.GetValue() / inteligencePotency);
        return baseSpellDamage;
    }

    public virtual void Die()
    {
        died = true;
    }
}
