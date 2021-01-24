
public class WeaponTreeSkill7Effect : PassiveSkillEffect
{
    //reload speed
    float percentagePerPoint = 5;
    PlayerAttack playerAttack;
    public override void Effect(PassiveSkill skill)
    {
        if (playerAttack)
        {
            IncreaseReloadSpeed(skill);
        }
        else
        {
            playerAttack = PlayerManager.instance.player.GetComponent<PlayerAttack>();
            IncreaseReloadSpeed(skill);
        }
    }

    private void IncreaseReloadSpeed(PassiveSkill skill)
    {
        Stat reloadSpeed = playerAttack.GetReloadSpeed();
        reloadSpeed.RemoveModifier(percentagePerPoint * (skill.points - 1));
        reloadSpeed.AddModifier(percentagePerPoint * skill.points);
    }
}
