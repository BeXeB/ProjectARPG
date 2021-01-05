using UnityEngine;

public class ActiveSkillUI : MonoBehaviour
{
    [SerializeField] Transform activeSkillsParent;
    ActiveSkillTreeController activeSkillTreeController;
    ActiveSkillUISlot[] slots;

    private void Start()
    {
        slots = activeSkillsParent.GetComponentsInChildren<ActiveSkillUISlot>();
        activeSkillTreeController = PlayerManager.instance.player.GetComponent<ActiveSkillTreeController>();
        activeSkillTreeController.onActiveSkillsChagedCallback += UpdateUI;
    }

    void UpdateUI(ActiveSkillTree newSkillTree)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < newSkillTree.skillTree.Length && newSkillTree.skillTree[i] != null)
            {
                slots[i].AddSkill(newSkillTree.skillTree[i]);
            }
            else
            {
                slots[i].CleatSlot();
            }
        }
    }
}
