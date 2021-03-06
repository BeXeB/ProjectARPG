public class TankTreeSkill2Effect : PassiveSkillEffect
{
    //add armor
    float perPoint = 1;
    private PlayerStats playerStats;
    public override void Effect(PassiveSkill skill)
    {
        if (playerStats)
        {
            IncreaseArmor(skill);
        }
        else
        {
            playerStats = PlayerManager.instance.player.GetComponent<PlayerStats>();
            IncreaseArmor(skill);
        }
    }

    private void IncreaseArmor(PassiveSkill skill)
    {
        Stat stat = playerStats.GetArmor();
        stat.RemoveModifier(perPoint * (skill.GetPoints() - 1));
        stat.AddModifier(perPoint * skill.GetPoints());
    }
}
