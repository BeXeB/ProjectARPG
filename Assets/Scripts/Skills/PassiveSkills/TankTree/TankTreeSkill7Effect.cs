using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankTreeSkill7Effect : PassiveSkillEffect
{
    //better armor    
    float perPoint = 10;

    private PlayerStats playerStats;
    public override void Effect(PassiveSkill skill)
    {
        if (playerStats)
        {
            IncreaseArmorPotency(skill);
        }
        else
        {
            playerStats = PlayerManager.instance.player.GetComponent<PlayerStats>();
            IncreaseArmorPotency(skill);
        }
    }

    private void IncreaseArmorPotency(PassiveSkill skill)
    {
        Stat strPot = playerStats.GetArmorPot();
        strPot.RemoveModifier(-(perPoint * (skill.GetPoints() - 1)));
        strPot.AddModifier(-(perPoint * skill.GetPoints()));
    }
}
