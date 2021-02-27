public class TankTreeSkill1Effect : PassiveSkillEffect
{
    //add vit
    private PlayerStats playerStats;
    float perPoint = 1;
    public override void Effect(PassiveSkill skill)
    {
        if (playerStats)
        {
            IncreaseVitality(skill);
        }
        else
        {
            playerStats = PlayerManager.instance.player.GetComponent<PlayerStats>();
            IncreaseVitality(skill);
        }
    }

    private void IncreaseVitality(PassiveSkill skill)
    {
        Stat stat = playerStats.GetVitality();
        stat.RemoveModifier((skill.points - 1) * perPoint);
        stat.AddModifier(skill.points * perPoint);
    }
}
