using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellTreeSkill9Effect : PassiveSkillEffect
{
    //10 stack, bonus stack duration
    bool activated = false;
    float stackIncrease = 10;
    float bonusToStackDurationPercentage = 100;
    SkillTreeStackController stackController;
    Stat stackDuration;

    public override void Effect(PassiveSkill skill)
    {
        if (!stackController)
        {
            stackController = PlayerManager.instance.player.GetComponent<SkillTreeStackController>();
            stackDuration = stackController.GetStackDuration();
        }

        IncreaseStackDuration(skill);
    }

    private void IncreaseStackDuration(PassiveSkill skill)
    {
        if (!activated)
        {
            activated = true;
            stackController.GetMaxStacks().AddModifier(stackIncrease);
            stackDuration.AddPercentageModifier(bonusToStackDurationPercentage);
        }
    }
}
