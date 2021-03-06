using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankTreeSkill6Effect : PassiveSkillEffect
{
    //shield capacity
    float percentPerPoint = 10;
    private PlayerStats playerStats;
    public override void Effect(PassiveSkill skill)
    {
        if (playerStats)
        {
            IncreaseShield(skill);
        }
        else
        {
            playerStats = PlayerManager.instance.player.GetComponent<PlayerStats>();
            IncreaseShield(skill);
        }
    }

    private void IncreaseShield(PassiveSkill skill)
    {
        Stat stat = playerStats.GetShield();
        stat.RemovePercentageModifier(percentPerPoint * (skill.GetPoints() - 1));
        stat.AddPercentageModifier(percentPerPoint * skill.GetPoints());
    }
}
