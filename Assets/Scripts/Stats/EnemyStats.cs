using UnityEngine;

[RequireComponent(typeof(DropTable))]
public class EnemyStats : CharacterStats
{
    public override void Die()
    {
        if (!died)
        {
            base.Die();
            GetComponent<DropTable>().Drop();
            GameObject.Destroy(gameObject, 1f);
        }
    }
}
