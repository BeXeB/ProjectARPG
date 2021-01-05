using UnityEngine;
using UnityEngine.UI;

public class PassiveSkillUISlot : MonoBehaviour
{
    [SerializeField] Image icon;
    PassiveSkill skill;

    public void AddSkill(PassiveSkill skill)
    {
        this.skill = skill;
        if (skill.unlocked)
        {
            icon.sprite = skill.unlockedIcon;
        }
        else
        {
            icon.sprite = skill.lockedIcon;
        }
        icon.enabled = true;
    }

    public void CleatSlot()
    {
        skill = null;
        icon.sprite = null;
        icon.enabled = false;
    }

    public void AddPoint()
    {
        if (skill.unlocked)
        {
            PlayerManager.instance.player.GetComponent<PassiveSkillTreeController>().TryAddSkillPoint(skill);
        }
    }
}
