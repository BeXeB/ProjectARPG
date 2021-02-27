using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellTreeSkill8Effect : PassiveSkillEffect
{
    //spells can crit
    PlayerStats playerStats;

    public override void Effect(PassiveSkill skill)
    {
        if (!playerStats)
        {
            playerStats = PlayerManager.instance.player.GetComponent<PlayerStats>();
        }
        EnableSpellCrit();
    }

    private void EnableSpellCrit()
    {
        playerStats.SetSpellCrit(true);
    }
}
