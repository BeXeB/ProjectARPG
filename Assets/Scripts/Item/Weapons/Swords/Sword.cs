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
                //calculate angle, ignore y component
                Quaternion rotation = Quaternion.FromToRotation(playerModel.forward, new Vector3(dir.x, 0, dir.z));
                Debug.Log(rotation.eulerAngles.y);
                float angle = rotation.eulerAngles.y;
                if (angle <= attackAngle || angle >= (360f - attackAngle)) //check angle
                {
                    //need a calculate damage method prob at stat
                    stats.TakeDamage(baseDmg);
                }
            }
        }
    }
}
