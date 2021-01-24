using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private PlayerStats playerStatsScript;

    private EquipmentController equipment;

    private float attackCooldown = 0f;

    private float reloadCooldown = 0f;

    Weapon weapon;

    Stat attackSpeed;

    Stat reloadSpeed;

    Stat magSize;

    #region getters/setters

    public Stat GetAttackSpeed()
    {
        return attackSpeed;
    }

    public Stat GetReloadSpeed()
    {
        return reloadSpeed;
    }

    public Stat GetMagSize()
    {
        return magSize;
    }

    #endregion

    private void Start()
    {
        playerStatsScript = GetComponent<PlayerStats>();
        equipment = GetComponent<EquipmentController>();
        equipment.onEquipmentChangedCallback += OnEquipmentChanged;
    }

    private void Update()
    {
        attackCooldown -= Time.deltaTime * (1 + (attackSpeed.GetValue() / 100));
        reloadCooldown -= Time.deltaTime * (1 + (reloadSpeed.GetValue() / 100));
    }
    public void Attack()
    {
        if (weapon && attackCooldown <= 0f && reloadCooldown <= 0f)
        {
            if (weapon.currentMag == 0)
            {
                weapon.currentMag = Mathf.FloorToInt(weapon.magSize * (1 + (magSize.GetValue() / 100)));
            }
            weapon.Attack(playerStatsScript.CalcWeaponDmg(weapon.baseDmg));
            if (--weapon.currentMag == 0)
            {
                Reload();
            }
            attackCooldown = 1f / weapon.attackSpeed;
        }
    }

    public void Reload()
    {
        reloadCooldown = weapon.reloadSpeed;
    }

    private void OnEquipmentChanged(Equipable newItem, Equipable oldItem)
    {
        if (newItem is Weapon)
        {
            weapon = (Weapon)newItem;
        }
    }
}
