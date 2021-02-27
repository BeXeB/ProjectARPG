using System;
using UnityEngine;

public class LevelSystem : MonoBehaviour
{
    public delegate void OnExperienceChaged(int ammount, int currentExperience);
    public OnExperienceChaged onExperienceChagedCallback;
    public delegate void OnLevelChanged();
    public OnLevelChanged onLevelChangedCallback;

    private int level = 1;
    private int experience = 0;
    [SerializeField] private int experienceToNextLevel = 1000;
    [SerializeField] private float experienceToNextLevelMultiplyer = 1.5f;

    public int GetExperienceToNextLevel()
    {
        return experienceToNextLevel;
    }

    public int GetExperience()
    {
        return experience;
    }

    public int GetLevel()
    {
        return level;
    }

    public void AddExperience(int ammount)
    {
        experience += ammount;
        if (experience >= experienceToNextLevel)
        {
            level++;
            experience -= experienceToNextLevel;
            experienceToNextLevel = Mathf.FloorToInt(experienceToNextLevel * experienceToNextLevelMultiplyer);
            if (onLevelChangedCallback != null)
            {
                onLevelChangedCallback.Invoke();
            }
        }
        if (onExperienceChagedCallback != null)
        {
            onExperienceChagedCallback.Invoke(ammount, experience);
        }
    }
}
