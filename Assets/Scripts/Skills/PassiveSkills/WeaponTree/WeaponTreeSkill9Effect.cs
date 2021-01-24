using UnityEngine;

public class WeaponTreeSkill9Effect : PassiveSkillEffect
{
    //execute
        
    float executePercent = 20;
    public override void Effect(PassiveSkill skill)
    {
        CharacterStats.onDamageTakenCallback += OnDamageTaken;
    }

    void OnDamageTaken(GameObject gameObject, float currentHealt, float maxHealth)
    {
        EnemyStats enemyStats = gameObject.GetComponent<EnemyStats>();
        if (enemyStats && ((currentHealt/maxHealth) * 100) <= executePercent)
        {
            enemyStats.Execute();
        }
    }
}
