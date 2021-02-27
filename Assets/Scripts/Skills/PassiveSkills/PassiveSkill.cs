using UnityEngine;

[CreateAssetMenu()]
public class PassiveSkill : ScriptableObject
{
    public float coolDown = 0f;
    public int points = 0;
    public int maxPoints = 0;
    public int bonusPoints = 0;
    public int tier = 0;
    public bool unlocked = false;
    public Sprite unlockedIcon;
    public Sprite lockedIcon;
    new public string name = "new skill";
    public GameObject skillEffect;
}
