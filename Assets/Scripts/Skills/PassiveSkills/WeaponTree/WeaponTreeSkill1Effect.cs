public class WeaponTreeSkill1Effect : PassiveSkillEffect
{
    //add strength
    private PlayerStats playerStats;
    public override void Effect(PassiveSkill skill)
    {
        if (playerStats)
        {
            IncreaseStrength(skill);
        }
        else
        {
            playerStats = PlayerManager.instance.player.GetComponent<PlayerStats>();
            IncreaseStrength(skill);
        }
    }

    private void IncreaseStrength(PassiveSkill skill)
    {
        Stat stat = playerStats.GetStrength();
        stat.RemoveModifier(skill.points - 1);
        stat.AddModifier(skill.points);
    }
}
