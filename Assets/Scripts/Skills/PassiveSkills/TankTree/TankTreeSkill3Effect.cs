using UnityEngine;

public class TankTreeSkill3Effect : PassiveSkillEffect
{
    private float percentPerPoint = 10;
    private float percentPerSec = 0;
    private Stat health;
    private PlayerStats playerStats;

    public override void Effect(PassiveSkill skill)
    {
        if (playerStats)
        {
            IncreaseHealthPerSec(skill);
        }
        else
        {
            playerStats = PlayerManager.instance.player.GetComponent<PlayerStats>();
            IncreaseHealthPerSec(skill);
        }
    }

    private void IncreaseHealthPerSec(PassiveSkill skill)
    {
        if (health == null)
        {
            health = playerStats.GetMaxHealth();
        }
        percentPerSec = percentPerPoint * skill.GetPoints();
    }

    private void Update()
    {
        if (!playerStats.GetDied())
        {
            playerStats.Heal((health.GetValue() - playerStats.currentHealt) * percentPerSec * Time.deltaTime);
        }

    }
}
