public class TankTreeSkill4Effect : PassiveSkillEffect
{
    float percentPerPoint = 10;
    private PlayerStats playerStats;
    public override void Effect(PassiveSkill skill)
    {
        if (playerStats)
        {
            IncreaseHealth(skill);
        }
        else
        {
            playerStats = PlayerManager.instance.player.GetComponent<PlayerStats>();
            IncreaseHealth(skill);
        }
    }

    private void IncreaseHealth(PassiveSkill skill)
    {
        Stat stat = playerStats.GetMaxHealth();
        stat.RemovePercentageModifier(percentPerPoint * (skill.points - 1));
        stat.AddPercentageModifier(percentPerPoint * skill.points);
    }
}
