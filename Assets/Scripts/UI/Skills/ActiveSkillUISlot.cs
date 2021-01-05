using UnityEngine;
using UnityEngine.UI;

public class ActiveSkillUISlot : MonoBehaviour
{
    [SerializeField] Image icon;
    [SerializeField] GameObject skillslots;
    ActiveSkill skill;

    public void AddSkill(ActiveSkill skill)
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

    public void ShowSkillSlots()
    {
        skillslots.SetActive(true);
    }

    public void SelectSkillSlot(int index)
    {
        if (skill.unlocked)
        {
            PlayerManager.instance.player.GetComponent<SkillBar>().EquipSkill(skill, index);
            skillslots.SetActive(false);
        }
    }
}
