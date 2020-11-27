using UnityEngine;

[CreateAssetMenu(fileName = "New Sword", menuName = "Inventory/Weapons/Sword")]
public class Sword : Melee
{
    public override void Attack()
    {
        base.Attack();
        Transform playerModel = PlayerManager.instance.player.transform.GetChild(0);
        var collidedWith = Physics.OverlapSphere(playerModel.position, reach);
        foreach (Collider collider in collidedWith)
        {
            var stats = collider.GetComponent<EnemyStats>();
            if (stats)
            {
                Vector3 dir = (collider.transform.position - playerModel.position).normalized;
                Debug.Log(playerModel.forward);
                Debug.DrawRay(playerModel.position, dir * 10f, Color.red, 50f);
                //calculate angle, ignore y component
                if (true) //check angle
                {
                    //need a calculate damage method prob at stat
                    stats.TakeDamage(baseDmg);
                }
            }
        }
    }
}
