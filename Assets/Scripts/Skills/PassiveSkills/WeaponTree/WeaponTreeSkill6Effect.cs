public class WeaponTreeSkill6Effect : PassiveSkillEffect
{
    //mag size
    float percentagePerPoint = 5;
    PlayerAttack playerAttack;
    public override void Effect(PassiveSkill skill)
    {
        if (playerAttack)
        {
            IncreaseMagSize(skill);
        }
        else
        {
            playerAttack = PlayerManager.instance.player.GetComponent<PlayerAttack>();
            IncreaseMagSize(skill);
        }
    }

    private void IncreaseMagSize(PassiveSkill skill)
    {
        Stat magSize = playerAttack.GetMagSize();
        magSize.RemoveModifier(percentagePerPoint * skill.points - 1);
        magSize.AddModifier(percentagePerPoint * skill.points);
    }
}
