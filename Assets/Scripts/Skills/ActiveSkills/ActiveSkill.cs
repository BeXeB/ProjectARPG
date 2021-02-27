using UnityEngine;

[CreateAssetMenu()]
public class ActiveSkill : ScriptableObject
{
    public WeaponType weaponType;
    public float baseDamage = 0f;
    public float coolDown = 0f;
    public float duration = -1f;
    public int levelToUnlock = 0;
    public bool unlocked = false;
    public Sprite unlockedIcon;
    public Sprite lockedIcon;
    new public string name = "new skill";
    public GameObject skillEffect;
}
