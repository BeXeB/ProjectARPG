using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellTreeSkill4Effect : PassiveSkillEffect
{
    //5% spelldamage per stack, 5 stack

    bool activated = false;
    float stackIncrease = 5;
    float percentPerPoint = 5;
    float currentPercentagePerStack = 0;
    float previousBonus = 0;
    SkillTreeStackController stackController;
    PlayerStats playerStats;
    Stat spellDamage;

    public override void Effect(PassiveSkill skill)
    {
        if (!stackController)
        {
            stackController = PlayerManager.instance.player.GetComponent<SkillTreeStackController>();
            stackController.onStacksChangedCallback += OnStacksChanged;
        }
        if (!playerStats)
        {
            playerStats = PlayerManager.instance.player.GetComponent<PlayerStats>();
            spellDamage = playerStats.GetSpellDamagePercentage();
        }
        IncreaseSpellDamage(skill);
    }

    private void OnStacksChanged(int stacks)
    {
        spellDamage.RemovePercentageModifier(previousBonus);
        spellDamage.AddPercentageModifier(currentPercentagePerStack * stacks);
        previousBonus = currentPercentagePerStack * stacks;
    }

    private void IncreaseSpellDamage(PassiveSkill skill)
    {
        if (!activated)
        {
            activated = true;
            stackController.GetMaxStacks().AddModifier(stackIncrease);
        }

        currentPercentagePerStack = skill.points * percentPerPoint;
    }
}
