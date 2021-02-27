using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WeaponExpBarUI : MonoBehaviour
{
    [SerializeField] Sprite[] weaponTypeImages;
    [SerializeField] Slider slider;
    [SerializeField] TMP_Text level;
    [SerializeField] Image weaponTypeIcon;
    [SerializeField] GameObject UI;
    WeaponLevelSystem weaponLevelSystem;
    WeaponType currentWeaponType;

    void Start()
    {
        weaponLevelSystem = PlayerManager.instance.player.GetComponent<WeaponLevelSystem>();
        PlayerManager.instance.player.GetComponent<EquipmentController>().onEquipmentChangedCallback += onEquipmentChanged;
        weaponLevelSystem.onWeaponExperienceChangedCallback += OnWeaponExperienceChanged;
        weaponLevelSystem.onWeaponLevelChangedCallback += OnWEaponLevelChanged;
        Weapon currenWeapon = (Weapon)PlayerManager.instance.player.GetComponent<EquipmentController>().GetEquipment()[(int)EquipmentSlot.Weapon];
        if (currenWeapon)
        {
            currentWeaponType = currenWeapon.weaponType;
            slider.maxValue = weaponLevelSystem.GetExperienceToNextLevel(currentWeaponType);
            slider.value = weaponLevelSystem.GetExperience(currentWeaponType);
            level.text = weaponLevelSystem.GetLevel(currentWeaponType).ToString();
            weaponTypeIcon.sprite = weaponTypeImages[(int)currentWeaponType];
        }
        else
        {
            UI.SetActive(false);
        }
    }

    void onEquipmentChanged(Equipable newItem, Equipable oldItem)
    {
        if (newItem is Weapon)
        {
            currentWeaponType = ((Weapon)newItem).weaponType;
            slider.maxValue = weaponLevelSystem.GetExperienceToNextLevel(currentWeaponType);
            slider.value = weaponLevelSystem.GetExperience(currentWeaponType);
            level.text = weaponLevelSystem.GetLevel(currentWeaponType).ToString();
            weaponTypeIcon.sprite = weaponTypeImages[(int)currentWeaponType];
            if (!UI.activeSelf)
            {
                UI.SetActive(true);
            }
        }
        if (newItem == null && oldItem is Weapon)
        {
            UI.SetActive(false);
        }
    }

    void OnWeaponExperienceChanged(WeaponType type, int experience)
    {
        if (type == currentWeaponType)
        {
            slider.value = experience;
        }
    }

    void OnWEaponLevelChanged(WeaponType type, int level)
    {
        if (type == currentWeaponType)
        {
            this.level.text = level.ToString();
            slider.maxValue = weaponLevelSystem.GetExperienceToNextLevel(type);
        }
    }
}
