using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellTreeSkill7Effect : PassiveSkillEffect
{
    //better inteligence per stack, 5 stack
    bool activated = false;
    float stackIncrease = 5;
    float bonusPerpoint = 10;
    float currentBonusPerStack = 0;
    float previousBonus = 0;
    SkillTreeStackController stackController;
    PlayerStats playerStats;
    Stat inteligencePotency;

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
            inteligencePotency = playerStats.GetIntPot();
        }
        IncreaseInteligencePotency(skill);
    }

    private void OnStacksChanged(int stacks)
    {
        inteligencePotency.RemoveModifier(previousBonus);
        inteligencePotency.AddModifier(currentBonusPerStack * stacks);
        previousBonus = currentBonusPerStack * stacks;
    }

    private void IncreaseInteligencePotency(PassiveSkill skill)
    {
        if (!activated)
        {
            activated = true;
            stackController.GetMaxStacks().AddModifier(stackIncrease);
        }

        currentBonusPerStack = skill.points * bonusPerpoint;
    }
}
