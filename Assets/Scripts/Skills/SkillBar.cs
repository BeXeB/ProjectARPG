using UnityEngine;

public class SkillBar : MonoBehaviour
{
    [SerializeField] private int skillNo = 4;
    public ActiveSkill[] skills;
    public float[] coolDowns;
    private PlayerStats playerStatsScript;
    private Equipment equipment;


    public delegate void OnSkillsChanged(ActiveSkill newSkill, ActiveSkill oldSkill);
    public OnSkillsChanged onSkillsChangedCallback;

    private void Start()
    {
        equipment = PlayerManager.instance.player.GetComponent<Equipment>();
        equipment.onEquipmentChangedCallback += OnWeaponChanged;
        skills = new ActiveSkill[skillNo];
        coolDowns = new float[skillNo];
        playerStatsScript = GetComponent<PlayerStats>();
    }

    public void OnWeaponChanged(Equipable newEquipment, Equipable oldEquipment)
    {
        if (newEquipment is Weapon)
        {
            for (int i = 0; i < skills.Length; i++)
            {
                if (skills[i] && skills[i].weaponType != ((Weapon)newEquipment).weaponType)
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
            if (skills[i] == newSkill)
            {
                UnequipSkill(i);
            }
        }
        skills[slot] = newSkill;
        if (onSkillsChangedCallback != null)
        {
            onSkillsChangedCallback.Invoke(newSkill, oldSkill);
        }
    }

    public ActiveSkill UnequipSkill(int slot)
    {
        ActiveSkill oldSkill = null;
        if (skills[slot] != null)
        {
            oldSkill = skills[slot];
            skills[slot] = null;
        }
        if (onSkillsChangedCallback != null)
        {
            onSkillsChangedCallback.Invoke(null, oldSkill);
        }
        return oldSkill;
    }

    private void Update()
    {
        for (int i = 0; i < coolDowns.Length; i++)
        {
            coolDowns[i] -= Time.deltaTime;
        }
    }

    public void TryUseSkill(int index)
    {
        if (coolDowns[index] <= 0f && skills[index] != null)
        {
            float damage = playerStatsScript.CalcSkillDmg(skills[index].baseDamage);
            skills[index].skillEffect.GetComponent<ActiveSkillEffect>().Effect(transform, damage);
            coolDowns[index] = skills[index].coolDown;
        }
    }

    public ActiveSkill[] GetSkills()
    {
        return skills;
    }
}
