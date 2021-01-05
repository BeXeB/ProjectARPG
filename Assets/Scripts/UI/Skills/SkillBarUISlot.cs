using UnityEngine;
using UnityEngine.UI;

public class SkillBarUISlot : MonoBehaviour
{
    [SerializeField] Image icon;
    ActiveSkill skill;

    public void AddSkill(ActiveSkill skill)
    {
        this.skill = skill;
        icon.sprite = skill.unlockedIcon;
        icon.enabled = true;
    }

    public void CleatSlot()
    {
        skill = null;
        icon.sprite = null;
        icon.enabled = false;
    }
}
