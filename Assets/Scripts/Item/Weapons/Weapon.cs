using System.Collections;
using UnityEngine;

public class Weapon : Equipable
{
    public int damageModifier = 0;
    public Type weaponType;
    public Range weaponRange;
    public int baseDmg = 0;
    public bool twoHanded = false;
    public int attackSpeed = 0;
    protected bool canAttack = true;
    public virtual void Attack()
    {
        Debug.Log("Attacking");
    }
}

public enum Type { Sword, Hammer, Axe, Sicle, Gauntlet, Pistol, SMG, Rifle, Sniper, LMG, Launcher }
public enum Range { Melee, Ranged }