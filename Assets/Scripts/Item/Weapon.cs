using UnityEngine;
using System.Collections.Generic;

public class Weapon : Equipable
{
    protected Transform playerModel;
    private Queue<ProjectileBase> pool = new Queue<ProjectileBase>();
    public GameObject weaponModel;
    public GameObject projectile;
    public int damageModifier = 0;
    public WeaponType weaponType;
    public int baseDmg = 0;
    public int attackSpeed = 0;
    public int currentMag = 0;
    public int magSize = 0;
    public int reloadSpeed = 0;
    public virtual void Attack(float calcDamage)
    {
        if (playerModel == null)
        {
            playerModel = PlayerManager.instance.player.transform;
        }
    }

    protected ProjectileBase Get()
    {
        if (pool.Count == 0)
        {
            AddProjectile(1);
        }
        return pool.Dequeue();
    }

    private void AddProjectile(int count)
    {
        for (int i = 0; i < count; i++)
        {
            ProjectileBase newProjectile = Instantiate(projectile).GetComponent<ProjectileBase>();
            newProjectile.gameObject.SetActive(false);
            pool.Enqueue(newProjectile);
        }
    }

    public void ReturnToPool(ProjectileBase projectile)
    {
        projectile.gameObject.SetActive(false);
        pool.Enqueue(projectile);
    }
}

public enum WeaponType { Pistol/*, SMG, Shotgun, Rifle, Sniper, Launcher*/ }