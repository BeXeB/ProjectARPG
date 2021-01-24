public class WeaponTreeSkill3Effect : PassiveSkillEffect
{
    //crit chance    
    float percentagePerPoint = 15;
    private PlayerStats playerStats;
    public override void Effect(PassiveSkill skill)
    {
        if (playerStats)
        {
            IncreaseCritChance(skill);
        }
        else
        {
            playerStats = PlayerManager.instance.player.GetComponent<PlayerStats>();
            IncreaseCritChance(skill);
        }
    }

    private void IncreaseCritChance(PassiveSkill skill)
    {
        Stat damageIncrease = playerStats.GetCritChance();
        damageIncrease.RemoveModifier(percentagePerPoint * skill.points -1);
        damageIncrease.AddModifier(percentagePerPoint * skill.points);
    }
}
