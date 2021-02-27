using UnityEngine;

public class SkillBar : MonoBehaviour
{
    [SerializeField] private int skillNo = 4;
    public SkillCD[] skills;
    private PlayerStats playerStatsScript;
    [SerializeField] private Stat durationBonus;
    private EquipmentController equipment;


    public delegate void OnSkillsChanged(ActiveSkill newSkill, ActiveSkill oldSkill);
    public OnSkillsChanged onSkillsChangedCallback;

    private void Start()
    {
        equipment = PlayerManager.instance.player.GetComponent<EquipmentController>();
        equipment.onEquipmentChangedCallback += OnWeaponChanged;
        skills = new SkillCD[skillNo];
        playerStatsScript = GetComponent<PlayerStats>();
    }

    public Stat GetDurationBonus()
    {
        return durationBonus;
    }

    public void OnWeaponChanged(Equipable newEquipment, Equipable oldEquipment)
    {
        if (newEquipment is Weapon)
        {
            for (int i = 0; i < skills.Length; i++)
            {
                if (skills[i].skill && skills[i].skill.weaponType != ((Weapon)newEquipment).weaponType)
                {
                    UnequipSkill(i);
                }
            }
        }
    }

    public void EquipSkill(ActiveSkill newSkill, int slot)
    {
        ActiveSkill oldSkill = UnequipSkill(slot);
        for (int i = 0; i < skills.Length; i++)
        {
            if (skills[i].skill == newSkill)
            {
                UnequipSkill(i);
            }
        }
        skills[slot].skill = newSkill;
        if (onSkillsChangedCallback != null)
        {
            onSkillsChangedCallback.Invoke(newSkill, oldSkill);
        }
    }

    public ActiveSkill UnequipSkill(int slot)
    {
        ActiveSkill oldSkill = null;
        if (skills[slot].skill != null)
        {
            oldSkill = skills[slot].skill;
            skills[slot].skill = null;
        }
        if (onSkillsChangedCallback != null)
        {
            onSkillsChangedCallback.Invoke(null, oldSkill);
        }
        return oldSkill;
    }

    private void Update()
    {
        for (int i = 0; i < skills.Length; i++)
        {
            skills[i].cd -= Time.deltaTime;
        }
    }

    public void TryUseSkill(int index)
    {
        if (skills[index].cd <= 0f && skills[index].skill != null)
        {
            float damage = playerStatsScript.CalcSkillDmg(skills[index].skill.baseDamage);
            skills[index].skill.skillEffect.GetComponent<ActiveSkillEffect>().Effect(skills[index].skill, transform, damage, durationBonus.GetValue());
            skills[index].cd = skills[index].skill.coolDown;
        }
    }

    public ActiveSkill[] GetSkills()
    {
        ActiveSkill[] activeSkills = new ActiveSkill[skills.Length]; 
        for (int i = 0; i < skills.Length; i++)
        {
            activeSkills[i] = skills[i].skill;
        }
        return activeSkills;
    }
}

[System.Serializable]
public struct SkillCD
{
    public ActiveSkill skill;
    public float cd;
}