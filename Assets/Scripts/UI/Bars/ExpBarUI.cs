using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ExpBarUI : MonoBehaviour
{
    [SerializeField] Slider slider;
    [SerializeField] TMP_Text level;

    LevelSystem levelSystem;

    private void Start()
    {
        levelSystem = PlayerManager.instance.player.GetComponent<LevelSystem>();
        slider.maxValue = levelSystem.GetExperienceToNextLevel();
        slider.value = levelSystem.GetExperience();
        level.text = levelSystem.GetLevel().ToString();
        levelSystem.onExperienceChagedCallback += OnExperienceChanged;
        levelSystem.onLevelChangedCallback += OnLevelUp;
    }

    private void OnExperienceChanged(int ammount, int currentExperience)
    {
        slider.value = currentExperience;
    }

    private void OnLevelUp()
    {
        slider.maxValue = levelSystem.GetExperienceToNextLevel();
        level.text = levelSystem.GetLevel().ToString();
    }
}
