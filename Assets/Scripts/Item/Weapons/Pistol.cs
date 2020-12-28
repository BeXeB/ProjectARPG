using UnityEngine;

[CreateAssetMenu(fileName = "New Pistol", menuName = "Inventory/Weapons/Pistol")]
public class Pistol : Weapon
{
    public override void Attack(float calcDamage)
    {
        base.Attack(calcDamage);
        calcDamage += baseDmg;
        Vector3 pos = new Vector3(playerModel.position.x, playerModel.position.y + 1.2f, playerModel.position.z + 0.5f);
        var shotProjectile = Instantiate(projectile, pos, playerModel.rotation);
        var shotProjectileScript = shotProjectile.GetComponent<ProjectileBase>();
        shotProjectileScript.SetDamage(calcDamage);
        Destroy(shotProjectile, 5f);
    }
}
