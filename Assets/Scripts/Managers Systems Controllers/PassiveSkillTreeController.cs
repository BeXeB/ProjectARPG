using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PassiveSkillTreeController : MonoBehaviour
{
    public delegate void OnPassiveSkillsChanged(PassiveSkillTree passiveSkillTree);
    public OnPassiveSkillsChanged onPassiveSkillsChagedCallback;
    [SerializeField] PassiveSkillTree[] skillTrees;
    public int availableSkillPoints = 0;

    private LevelSystem levelSystem;

    private void Start()
    {
        levelSystem = PlayerManager.instance.player.GetComponent<LevelSystem>();
        levelSystem.onLevelChangedCallback += OnlevelChanged;
        StartCoroutine(LoadPassivesTest());
    }

    //dont forget to get rid of this
    IEnumerator LoadPassivesTest()
    {
        yield return new WaitForSeconds(0.1f);
        foreach (PassiveSkillTree tree in skillTrees)
        {
            onPassiveSkillsChagedCallback.Invoke(tree);
        }
    }

    void OnlevelChanged()
    {
        availableSkillPoints++;
    }

    public void TryAddSkillPoint(PassiveSkill skill)
    {
        if (availableSkillPoints > 0)
        {
            foreach (PassiveSkillTree tree in skillTrees)
            {
                foreach (PassiveSkill pskill in tree.skillTree)
                {
                    if (pskill == skill && pskill.points < pskill.maxPoints)
                    {
                        pskill.points++;
                        tree.pointsSpent++;
                        availableSkillPoints--;
                        if (tree.pointsSpent > tree.tier * tree.pointsPerTier)
                        {
                            tree.tier++;
                            UnlockSkillsInNextTier(tree);
                        }
                        onPassiveSkillsChagedCallback?.Invoke(tree);
                    }
                }
            }
        }
    }

    void UnlockSkillsInNextTier(PassiveSkillTree tree)
    {
        foreach (PassiveSkill skill in tree.skillTree)
        {
            if (skill.tier <= tree.tier && !skill.unlocked)
            {
                skill.unlocked = true;
            }
        }
    }
}
