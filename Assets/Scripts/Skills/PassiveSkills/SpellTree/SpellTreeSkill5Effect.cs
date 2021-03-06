using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellTreeSkill5Effect : PassiveSkillEffect
{
    //stacks better dext, 5 stack
    bool activated = false;
    float stackIncrease = 5;
    float bonusPerpoint = 5;
    float currentBonusPerStack = 0;
    float previousBonus = 0;
    SkillTreeStackController stackController;
    PlayerStats playerStats;
    Stat dexterityPotency;

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
            dexterityPotency = playerStats.GetDexPot();
        }
        DecreaseDexterityPotency(skill);
    }

    private void OnStacksChanged(int stacks)
    {
        dexterityPotency.RemoveModifier(-previousBonus);
        dexterityPotency.AddModifier(-(currentBonusPerStack * stacks));
        previousBonus = currentBonusPerStack * stacks;
    }

    private void DecreaseDexterityPotency(PassiveSkill skill)
    {
        if (!activated)
        {
            activated = true;
            stackController.GetMaxStacks().AddModifier(stackIncrease);
        }

        currentBonusPerStack = skill.GetPoints() * bonusPerpoint;
    }
}
