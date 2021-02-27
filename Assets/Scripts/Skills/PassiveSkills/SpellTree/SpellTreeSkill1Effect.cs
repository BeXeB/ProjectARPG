using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellTreeSkill1Effect : PassiveSkillEffect
{
    private PlayerStats playerStats;
    float perPoint = 1;
    public override void Effect(PassiveSkill skill)
    {
        if (playerStats)
        {
            IncreaseInteligence(skill);
        }
        else
        {
            playerStats = PlayerManager.instance.player.GetComponent<PlayerStats>();
            IncreaseInteligence(skill);
        }
    }

    private void IncreaseInteligence(PassiveSkill skill)
    {
        Stat stat = playerStats.GetIntelligence();
        stat.RemoveModifier((skill.points - 1) * perPoint);
        stat.AddModifier(skill.points * perPoint);
    }
}
