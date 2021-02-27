public class WeaponTreeSkill2Effect : PassiveSkillEffect
{
    //bonus base damage
    float percentagePerPoint = 1;
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
        Stat damageIncrease = playerStats.GetDamage();
        damageIncrease.AddPercentageModifier(percentagePerPoint);
    }
}
