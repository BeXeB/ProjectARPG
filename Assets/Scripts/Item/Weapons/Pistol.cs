using UnityEngine;

public class Pistol : Weapon
{
    public override void Attack(float calcDamage)
    {
        base.Attack(calcDamage);
        Vector3 pos = new Vector3(playerModel.position.x, playerModel.position.y + 1.2f, playerModel.position.z + 0.5f);
        var shotProjectile = GetProjectile();
        shotProjectile.transform.position = pos;
        shotProjectile.transform.forward = playerModel.forward; 
        shotProjectile.SetDamage(calcDamage);
        shotProjectile.SetWeaponShotFrom(this);
        shotProjectile.gameObject.SetActive(true);
    }
}
