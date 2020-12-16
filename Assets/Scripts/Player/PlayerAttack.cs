using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private PlayerStats playerStatsScript;

    private float attackCooldown = 0f;

    private void Start()
    {
        playerStatsScript = GetComponent<PlayerStats>();
    }

    private void Update()
    {
        attackCooldown -= Time.deltaTime;
    }
    public void Attack()
    {
        var weapon = (Weapon)Equipment.instance.GetEquipment()[8];
        if (weapon && attackCooldown <= 0f)
        {
            weapon.Attack(playerStatsScript.CalcWeaponDmg());
            attackCooldown = 1f / weapon.attackSpeed;
        }
    }
}
