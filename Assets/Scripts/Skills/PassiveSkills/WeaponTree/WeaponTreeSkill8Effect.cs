public class WeaponTreeSkill8Effect : PassiveSkillEffect
{
    //attack speed
    float percentagePerPoint = 100;
    PlayerAttack playerAttack;
    public override void Effect(PassiveSkill skill)
    {
        if (playerAttack)
        {
            IncreaseAttackSpeed(skill);
        }
        else
        {
            playerAttack = PlayerManager.instance.player.GetComponent<PlayerAttack>();
            IncreaseAttackSpeed(skill);
        }
    }

    private void IncreaseAttackSpeed(PassiveSkill skill)
    {
        Stat attackSpeed = playerAttack.GetAttackSpeed();
        attackSpeed.RemoveModifier(percentagePerPoint * (skill.points - 1));
        attackSpeed.AddModifier(percentagePerPoint * skill.points);
    }
}
