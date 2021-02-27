using UnityEngine;
public class ActiveSkillEffect : MonoBehaviour
{
    public virtual void Effect(ActiveSkill skill, Transform playerTransform, float calculatedDamage, float durationBonus)
    {
        print("haha");
    }
}