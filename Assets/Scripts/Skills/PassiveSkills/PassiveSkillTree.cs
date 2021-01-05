using UnityEngine;

[CreateAssetMenu()]
public class PassiveSkillTree : ScriptableObject
{
    new public string name = "New Passive Skill Tree";
    public int treeIndex = -1;
    public int pointsPerTier = 5;
    public int tier = 1;
    public int  pointsSpent = 0;
    public PassiveSkill[] skillTree;
}
