public class TankTreeSkill5Effect : PassiveSkillEffect
{
    //better vitality
    float perPoint = 2;

    private PlayerStats playerStats;
    public override void Effect(PassiveSkill skill)
    {
        if (playerStats)
        {
            IncreaseVitalityPotency(skill);
        }
        else
        {
            playerStats = PlayerManager.instance.player.GetComponent<PlayerStats>();
            IncreaseVitalityPotency(skill);
        }
    }

    private void IncreaseVitalityPotency(PassiveSkill skill)
    {
        Stat strPot = playerStats.GetVitPot();
        strPot.RemoveModifier(perPoint * (skill.points - 1));
        strPot.AddModifier(perPoint * skill.points);
    }
}
