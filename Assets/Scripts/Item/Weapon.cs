using UnityEngine;

public class Weapon : Equipable
{
    protected Transform playerModel;
    public int damageModifier = 0;
    public Type weaponType;
    public int baseDmg = 0;
    public int attackSpeed = 0;
    public int currentMag = 0;
    public int magSize = 0;
    public int reloadSpeed = 0;
    public GameObject projectile;
    public virtual void Attack(float calcDamage)
    {
        if (playerModel == null)
        {
            playerModel = PlayerManager.instance.player.transform;
        }
    }
}

public enum Type { Pistol/*, SMG, Shotgun, Rifle, Sniper, LMG, Launcher*/ }