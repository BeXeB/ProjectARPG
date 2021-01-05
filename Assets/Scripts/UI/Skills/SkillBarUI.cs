using UnityEngine;

public class SkillBarUI : MonoBehaviour
{
    [SerializeField] Transform skillBarParent;

    SkillBar skillBar;

    SkillBarUISlot[] slots;

    private void Start()
    {
        skillBar = PlayerManager.instance.player.GetComponent<SkillBar>();
        skillBar.onSkillsChangedCallback += UpdateUI;
        slots = skillBarParent.GetComponentsInChildren<SkillBarUISlot>();
    }

    void UpdateUI(ActiveSkill newSkill, ActiveSkill oldSkill)
    {
        var currentSkills = skillBar.GetSkills();
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < currentSkills.Length && currentSkills[i] != null)
            {
                slots[i].AddSkill(currentSkills[i]);
            }
            else
            {
                slots[i].CleatSlot();
            }
        }
    }
}
