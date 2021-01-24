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
                * weaponLevels[index].experienceToNextLevelMultiplyer);
            if (onWeaponLevelChangedCallback != null)
            {
                onWeaponLevelChangedCallback.Invoke(weaponType, weaponLevels[index].level);
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
