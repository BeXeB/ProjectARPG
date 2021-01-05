using UnityEngine;

[CreateAssetMenu()]
public class ActiveSkillTree : ScriptableObject
{
    new public string name = "New Acive Skill Tree";
    public ActiveSkill[] skillTree;
}
