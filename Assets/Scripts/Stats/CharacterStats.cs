using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    [SerializeField]
    private float armorPotency = 100f;
    public Stat intelligence;
    public Stat strength;
    public Stat vitality;
    public Stat dexterity;
    public Stat armor;
    public Stat damage;

    public int maxHealth = 100;
    public int currentHealt { get; private set; }

    private void Awake()
    {
        currentHealt = maxHealth;
    }

    public void TakeDamage(int incDmg)
    {
        //reduce dmg with armor
        //NewDmg = Dmg/(1+(AH/100))
        int reducedIncDmg = (int)Mathf.Round(incDmg/(1+(armor.GetValue()/armorPotency)));
        currentHealt -= reducedIncDmg;
        Debug.Log(transform.name + " takes " + incDmg + " damage");

        if (currentHealt <= 0)
        {
            Die();
        }
    }

    public virtual void Die()
    {
        Debug.Log(transform.name + " Dies");
    }
}
