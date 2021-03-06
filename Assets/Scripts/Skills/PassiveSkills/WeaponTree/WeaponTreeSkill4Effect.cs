public class WeaponTreeSkill4Effect : PassiveSkillEffect
{
    //critdamage
    float percentagePerPoint = 10;
    private PlayerStats playerStats;
    public override void Effect(PassiveSkill skill)
    {
        if (playerStats)
        {
            IncreaseCritDamage(skill);
        }
        else
        {
            playerStats = PlayerManager.instance.player.GetComponent<PlayerStats>();
            IncreaseCritDamage(skill);
        }
    }

    private void IncreaseCritDamage(PassiveSkill skill)
    {
        Stat damageIncrease = playerStats.GetCritDamage();
        damageIncrease.RemoveModifier(percentagePerPoint * (skill.GetPoints() - 1));
        damageIncrease.AddModifier(percentagePerPoint * skill.GetPoints());
    }
}
