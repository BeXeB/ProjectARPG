using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellTreeSkill3Effect : PassiveSkillEffect
{
    //10% spelldamage 
    private PlayerStats playerStats;
    float percentagePerPoint = 10;
    public override void Effect(PassiveSkill skill)
    {
        if (playerStats)
        {
            IncreaseBaseSpellDamage(skill);
        }
        else
        {
            playerStats = PlayerManager.instance.player.GetComponent<PlayerStats>();
            IncreaseBaseSpellDamage(skill);
        }
    }

    private void IncreaseBaseSpellDamage(PassiveSkill skill)
    {
        Stat stat = playerStats.GetSpellDamagePercentage();
        stat.RemoveModifier((skill.GetPoints() - 1) * percentagePerPoint);
        stat.AddModifier(skill.GetPoints() * percentagePerPoint);
    }
}
