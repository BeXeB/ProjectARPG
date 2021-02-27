using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellTreeSkill6Effect : PassiveSkillEffect
{
    //spell duration
    private SkillBar skillBar;
    float percentPerPoint = 10;
    public override void Effect(PassiveSkill skill)
    {
        if (skillBar)
        {
            IncreaseSpellDuration(skill);
        }
        else
        {
            skillBar = PlayerManager.instance.player.GetComponent<SkillBar>();
            IncreaseSpellDuration(skill);
        }
    }

    private void IncreaseSpellDuration(PassiveSkill skill)
    {
        Stat stat = skillBar.GetDurationBonus();
        stat.RemoveModifier((skill.points - 1) * percentPerPoint);
        stat.AddModifier(skill.points * percentPerPoint);
    }
}
