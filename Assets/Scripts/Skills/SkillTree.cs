using UnityEngine;
using System;

public class SkillTree : MonoBehaviour
{
    //passive skills
    private int skillpoints = 0;
    private int availableskillpoints = 0;
    private LevelSystem levelSystem;

    private void Start()
    {
        levelSystem = PlayerManager.instance.player.GetComponent<LevelSystem>();
        levelSystem.onLevelChangedCallback += OnlevelChanged;
    }

    void OnlevelChanged()
    {
        skillpoints++;
        availableskillpoints++;
    }
}