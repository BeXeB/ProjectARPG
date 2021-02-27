using UnityEngine;

[RequireComponent(typeof(DropTable))]
public class EnemyStats : CharacterStats
{
    public int experience = 100;
    public override void Die()
    {
        if (!died)
        {
            base.Die();
            if (died)
            {
                PlayerManager.instance.player.GetComponent<LevelSystem>().AddExperience(experience);
                GetComponent<DropTable>().Drop();
                GameObject.Destroy(gameObject, 1f);
            }
        }
    }
}
