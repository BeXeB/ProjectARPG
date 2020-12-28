using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private PlayerStats playerStatsScript;

    private float attackCooldown = 0f;

    private float reloadCooldown = 0f;

    private void Start()
    {
        playerStatsScript = GetComponent<PlayerStats>();
    }

    private void Update()
    {
        attackCooldown -= Time.deltaTime;
        reloadCooldown -= Time.deltaTime;
    }
    public void Attack()
    {
        var weapon = (Weapon)PlayerManager.instance.player.GetComponent<Equipment>().GetEquipment()[8];
        if (weapon && attackCooldown <= 0f && reloadCooldown <= 0f)
        {
            if (weapon.currentMag == 0)
            {
                weapon.currentMag = weapon.magSize;
            }
            weapon.Attack(playerStatsScript.CalcWeaponDmg());
            if(--weapon.currentMag == 0)
            {
                reloadCooldown = weapon.reloadSpeed;
            }
            attackCooldown = 1f / weapon.attackSpeed;
        }
    }
}
