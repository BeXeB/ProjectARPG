public class WeaponTreeSkill2Effect : PassiveSkillEffect
{
    //bonus base damage
    float percentagePerPoint = 10;
    private PlayerStats playerStats;
    public override void Effect(PassiveSkill skill)
    {
        if (playerStats)
        {
            IncreaseBaseDamage(skill);
        }
        else
        {
            playerStats = PlayerManager.instance.player.GetComponent<PlayerStats>();
            IncreaseBaseDamage(skill);
        }
    }

    private void IncreaseBaseDamage(PassiveSkill skill)
    {
        Stat damageIncrease = playerStats.GetDamageIncreasePercentage();
        damageIncrease.RemoveModifier(percentagePerPoint * skill.points -1);
        damageIncrease.AddModifier(percentagePerPoint * skill.points);
    }
}
