using System.Collections.Generic;
using UnityEngine;

public class PassiveSkillUI : MonoBehaviour
{
    [SerializeField] Transform passiveSkillsParent;
    PassiveSkillTreeController passiveSkillTreeController;
    List<PassiveSkillUISlot[]> slots = new List<PassiveSkillUISlot[]>();

    private void Start()
    {
        slots.Add(passiveSkillsParent.GetChild(0).GetComponentsInChildren<PassiveSkillUISlot>());
        slots.Add(passiveSkillsParent.GetChild(1).GetComponentsInChildren<PassiveSkillUISlot>());
        slots.Add(passiveSkillsParent.GetChild(2).GetComponentsInChildren<PassiveSkillUISlot>());
        passiveSkillTreeController = PlayerManager.instance.player.GetComponent<PassiveSkillTreeController>();
        passiveSkillTreeController.onPassiveSkillsChagedCallback += UpdateUI;
    }

    void UpdateUI(PassiveSkillTree newSkillTree)
    {
        for (int i = 0; i < slots[newSkillTree.treeIndex].Length; i++)
        {
            if (i < newSkillTree.skillTree.Length && newSkillTree.skillTree[i] != null)
            {
                slots[newSkillTree.treeIndex][i].AddSkill(newSkillTree.skillTree[i]);
            }
            else
            {
                slots[newSkillTree.treeIndex][i].CleatSlot();
            }
        }
    }
}
