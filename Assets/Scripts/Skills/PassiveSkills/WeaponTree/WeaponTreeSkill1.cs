using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponTreeSkill1 : PassiveSkillEffect
{
    //add strength
    private PlayerStats playerStats;
    private void Start() {
        playerStats = PlayerManager.instance.player.GetComponent<PlayerStats>();
    }
    public override void Effect(PassiveSkill skill)
    {
        base.Effect(skill);
        Stat stat = playerStats.getStrength();
        stat.RemoveModifier(skill.points - 1);
        stat.AddModifier(skill.points);
    }
}
