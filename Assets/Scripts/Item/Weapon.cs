using UnityEngine;

public class Weapon : Equipable
{
    public int damageModifier = 0;
    public Type weaponType;
    public int baseDmg = 0;
    public int attackSpeed = 0;
    public int magSize = 0;
    public int reloadSpeed = 0;
    public virtual void Attack()
    {
        Debug.Log("Attacking");
    }
}

public enum Type { Pistol/*, SMG, Rifle, Sniper, LMG, Launcher*/ }