using UnityEngine;

public class ActiveSkillTreeController : MonoBehaviour
{
    public delegate void OnActiveSkillsChanged(ActiveSkillTree newActiveSkillTree);
    public OnActiveSkillsChanged onActiveSkillsChagedCallback;
    [SerializeField] ActiveSkillTree[] skillTrees;
    EquipmentController equipment;
    WeaponLevelSystem weaponLevelSystem;
    public int activeSkillTreeIndex;

    private void Start()
    {
        weaponLevelSystem = PlayerManager.instance.player.GetComponent<WeaponLevelSystem>();
        equipment = PlayerManager.instance.player.GetComponent<EquipmentController>();
        weaponLevelSystem.onWeaponLevelChangedCallback += OnWeaponLevelChanged;
        equipment.onEquipmentChangedCallback += OnWeaponChanged;
    }

    private void OnWeaponChanged(Equipable newWeapon, Equipable oldWeapon)
    {
        if (newWeapon && newWeapon is Weapon && (int)((Weapon)newWeapon).weaponType != activeSkillTreeIndex)
        {
            activeSkillTreeIndex = (int)((Weapon)newWeapon).weaponType;
            if (onActiveSkillsChagedCallback != null)
            {
                onActiveSkillsChagedCallback.Invoke(skillTrees[activeSkillTreeIndex]);
            }
        }
    }

    private void OnWeaponLevelChanged(WeaponType weaponType, int level)
    {
        if ((int)weaponType == activeSkillTreeIndex)
        {
            foreach (ActiveSkill skill in skillTrees[activeSkillTreeIndex].skillTree)
            {
                if (skill.levelToUnlock <= level  && !skill.unlocked)
                {
                    skill.unlocked = true;
                    if (onActiveSkillsChagedCallback != null)
                    {
                        onActiveSkillsChagedCallback.Invoke(skillTrees[activeSkillTreeIndex]);
                    }
                }
            }
        }
    }
}
