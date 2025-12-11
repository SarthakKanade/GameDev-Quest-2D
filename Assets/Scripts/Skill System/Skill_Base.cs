using UnityEngine;

public class Skill_Base : MonoBehaviour
{
    [Header("General Details")]
    [SerializeField] protected SkillType skillType;
    [SerializeField] protected SkillUpgradeType skillUpgradeType;
    [SerializeField] private float cooldown;
    private float lastTimeUsed;

    protected virtual void Awake()
    {
        lastTimeUsed -= cooldown;
    }

    public void SetSkillUpgrade(SkillUpgradeData upgradeData)
    {
        skillUpgradeType = upgradeData.upgradeType;
        cooldown = upgradeData.cooldown;
    }

    protected bool Unlocked(SkillUpgradeType upgradeToCheck)
    {
        return skillUpgradeType == upgradeToCheck;
    }

    public bool CanUseSkill()
    {
        if (isOnCooldown)
        {
            Debug.Log("Skill is on cooldown");
            return false;
        }

        return true;
    }

    private bool isOnCooldown => Time.time < lastTimeUsed + cooldown;

    public void SetSkillOnCooldown() => lastTimeUsed = Time.time;

    public void ResetCoolDownBy(float cooldownReduction) => lastTimeUsed += cooldownReduction;

    public void ResetCooldown() => lastTimeUsed = Time.time;
   

}
