public class WeaponTreeSkill5Effect : PassiveSkillEffect
{
    //stength potency
    float perPoint = 10;

    private PlayerStats playerStats;
    public override void Effect(PassiveSkill skill)
    {
        if (playerStats)
        {
            IncreaseStrengthPotency(skill);
        }
        else
        {
            playerStats = PlayerManager.instance.player.GetComponent<PlayerStats>();
            IncreaseStrengthPotency(skill);
        }
    }

    private void IncreaseStrengthPotency(PassiveSkill skill)
    {
        Stat strPot = playerStats.GetStrPot();
        strPot.RemoveModifier(-(perPoint * (skill.points - 1)));
        strPot.AddModifier(-(perPoint * skill.points));
    }
}
