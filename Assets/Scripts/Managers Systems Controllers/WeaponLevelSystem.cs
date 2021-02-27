using UnityEngine;


public class WeaponLevelSystem : MonoBehaviour
{
    public delegate void OnWeaponExperienceChanged(WeaponType type, int currentExperience);
    public OnWeaponExperienceChanged onWeaponExperienceChangedCallback;
    public delegate void OnWeaponLevelChanged(WeaponType weaponType, int level);
    public OnWeaponLevelChanged onWeaponLevelChangedCallback;
    public int experienceToNextLevel = 1000;
    public float experienceToNextLevelMultiplyer = 2.5f;
    public WeaponLevel[] weaponLevels;
    private LevelSystem levelSystem;

    public int GetExperienceToNextLevel(WeaponType weaponType)
    {
        return weaponLevels[(int)weaponType].experienceToNextLevel;
    }

    public int GetExperience(WeaponType weaponType)
    {
        return weaponLevels[(int)weaponType].experience;
    }

    public int GetLevel(WeaponType weaponType)
    {
        return weaponLevels[(int)weaponType].level;
    }

    private void Start()
    {
        levelSystem = PlayerManager.instance.player.GetComponent<LevelSystem>();
        levelSystem.onExperienceChagedCallback += OnExperienceGained;
        weaponLevels = new WeaponLevel[System.Enum.GetNames(typeof(WeaponType)).Length];
        for (int i = 0; i < System.Enum.GetNames(typeof(WeaponType)).Length; i++)
        {
            weaponLevels[i].experienceToNextLevel = experienceToNextLevel;
            weaponLevels[i].level = 1;
        }
    }

    void OnExperienceGained(int ammount, int currentExperience)
    {
        WeaponType weaponType = ((Weapon)PlayerManager.instance.player
            .GetComponent<EquipmentController>().GetEquipment()[8]).weaponType;
        AddExperience(ammount, weaponType);
    }

    public void AddExperience(int ammount, WeaponType weaponType)
    {
        int index = (int)weaponType;
        weaponLevels[index].experience += ammount;
        if (weaponLevels[index].experience >= weaponLevels[index].experienceToNextLevel)
        {
            weaponLevels[index].level++;
            weaponLevels[index].experience -= weaponLevels[index].experienceToNextLevel;
            weaponLevels[index].experienceToNextLevel =
            Mathf.FloorToInt(weaponLevels[index].experienceToNextLevel
                * experienceToNextLevelMultiplyer);
            if (onWeaponLevelChangedCallback != null)
            {
                onWeaponLevelChangedCallback.Invoke(weaponType, weaponLevels[index].level);
            }
        }
        onWeaponExperienceChangedCallback?.Invoke(weaponType, weaponLevels[index].experience);
    }
}

[System.Serializable]
public struct WeaponLevel
{
    public int level;
    public int experience;
    public int experienceToNextLevel;
}
