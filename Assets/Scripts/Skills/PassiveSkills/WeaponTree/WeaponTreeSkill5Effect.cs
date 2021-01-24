public class WeaponTreeSkill5Effect : PassiveSkillEffect
{
    //stength potency
    float percentagePerPoint = 10;

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
        strPot.RemoveModifier(percentagePerPoint * (skill.points - 1));
        strPot.AddModifier(percentagePerPoint * skill.points);
    }
}
