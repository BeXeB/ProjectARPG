using UnityEngine;


public class WeaponLevelSystem : MonoBehaviour
{
    public int experienceToNextLevel = 1000;
    public float experienceToNextLevelMultiplyer = 2.5f;
    public delegate void OnWeapinLevelChanged(WeaponType weaponType, int level);
    public OnWeapinLevelChanged onWeaponLevelChangedCallback;
    public WeaponLevel[] weaponLevels;
    private LevelSystem levelSystem;
    private void Start()
    {
        levelSystem = PlayerManager.instance.player.GetComponent<LevelSystem>();
        levelSystem.onExperienceChagedCallback += OnExperienceGained;
        weaponLevels = new WeaponLevel[System.Enum.GetNames(typeof(WeaponType)).Length];
        for (int i = 0; i < System.Enum.GetNames(typeof(WeaponType)).Length; i++)
        {
            weaponLevels[i].experienceToNextLevel = experienceToNextLevel;
            weaponLevels[i].experienceToNextLevelMultiplyer = experienceToNextLevelMultiplyer;
        }
    }

    void OnExperienceGained(int ammount)
    {
        WeaponType weaponType = ((Weapon)PlayerManager.instance.player.GetComponent<Equipment>().GetEquipment()[8]).weaponType;
        AddExperience(ammount, weaponType);
    }

    public void AddExperience(int ammount, WeaponType weaponType)
    {
        weaponLevels[(int)weaponType].experience += ammount;
        if (weaponLevels[(int)weaponType].experience >= weaponLevels[(int)weaponType].experienceToNextLevel)
        {
            weaponLevels[(int)weaponType].level++;
            weaponLevels[(int)weaponType].experience -= weaponLevels[(int)weaponType].experienceToNextLevel;
            weaponLevels[(int)weaponType].experienceToNextLevel = Mathf.FloorToInt(weaponLevels[(int)weaponType].experienceToNextLevel * weaponLevels[(int)weaponType].experienceToNextLevelMultiplyer);
            if (onWeaponLevelChangedCallback != null)
            {
                onWeaponLevelChangedCallback.Invoke(weaponType, weaponLevels[(int)weaponType].level);
            }
        }
    }
}

[System.Serializable]
public struct WeaponLevel
{
    public int level;
    public int experience;
    public int experienceToNextLevel;
    public float experienceToNextLevelMultiplyer;
}
