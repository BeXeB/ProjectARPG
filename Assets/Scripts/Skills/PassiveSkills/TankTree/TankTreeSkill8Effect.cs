using UnityEngine;

public class TankTreeSkill8Effect : PassiveSkillEffect
{
    //Shield recharge
    float coolDown = 0;
    private PlayerStats playerStats;
    private PassiveSkill skill;
    public override void Effect(PassiveSkill skill)
    {
        PlayerStats.onShieldBreakCallback += OnShiledBreak;
        if (!playerStats)
        {
            playerStats = PlayerManager.instance.player.GetComponent<PlayerStats>();
        }
        if (!this.skill)
        {
            this.skill = skill;
        }
    }

    void OnShiledBreak()
    {
        if (coolDown <= 0f)
        {
            playerStats.RefillShield();
            coolDown = skill.coolDown;
        }
    }

    private void Update()
    {
        coolDown -= Time.deltaTime;
    }
}
