using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellTreeSkill2Effect : PassiveSkillEffect
{
    private PlayerStats playerStats;
    float perPoint = 1;
    public override void Effect(PassiveSkill skill)
    {
        if (playerStats)
        {
            IncreaseDexterity(skill);
        }
        else
        {
            playerStats = PlayerManager.instance.player.GetComponent<PlayerStats>();
            IncreaseDexterity(skill);
        }
    }

    private void IncreaseDexterity(PassiveSkill skill)
    {
        Stat stat = playerStats.GetDexterity();
        stat.RemoveModifier((skill.points - 1) * perPoint);
        stat.AddModifier(skill.points * perPoint);
    }
}
