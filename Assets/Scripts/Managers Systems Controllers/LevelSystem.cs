using System;
using UnityEngine;

public class LevelSystem : MonoBehaviour
{
    public delegate void OnExperienceChaged(int ammount);
    public OnExperienceChaged onExperienceChagedCallback;
    public delegate void OnLevelChanged();
    public OnLevelChanged onLevelChangedCallback;

    private int level = 0;
    private int experience = 0;
    [SerializeField] private int experienceTioNextLevel = 1000;
    [SerializeField] private float experienceToNextLevelMultiplyer = 1.5f;


    public void AddExperience(int ammount)
    {
        experience += ammount;
        if (experience >= experienceTioNextLevel)
        {
            level++;
            experience -= experienceTioNextLevel;
            experienceTioNextLevel = Mathf.FloorToInt(experienceTioNextLevel * experienceToNextLevelMultiplyer);
            if (onLevelChangedCallback != null) 
            {
                onLevelChangedCallback.Invoke();
            }
        }
        if (onExperienceChagedCallback != null)
        {
            onExperienceChagedCallback.Invoke(ammount);
        }
    }

    public int GetLevel()
    {
        return level;
    }
}
