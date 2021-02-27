using UnityEngine;

public class TankTreeSkill9Effect : PassiveSkillEffect
{
    //dont die
    float coolDown = 0;
    PassiveSkill skill;
    public override void Effect(PassiveSkill skill)
    {
        PlayerStats.onDiedCallback += OnDied;
        if (!this.skill)
        {
            this.skill = skill;
        }
    }

    void OnDied(GameObject gameObject)
    {
        PlayerStats playerStats = gameObject.GetComponent<PlayerStats>();
        if (playerStats && coolDown <= 0)
        {
            playerStats.SetDied(false);
            playerStats.Heal(playerStats.GetMaxHealth().GetValue());
            coolDown = skill.coolDown;
        }
    }

    private void Update()
    {
        coolDown -= Time.deltaTime;
    }
}
