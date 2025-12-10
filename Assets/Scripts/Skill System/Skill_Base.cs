using UnityEngine;

public class Skill_Base : MonoBehaviour
{
    [Header("General Details")]
    [SerializeField] protected SkillType skillType;
    [SerializeField] protected SkillUpgadeType skillUpgadeType;
    [SerializeField] private float cooldown;
    private float lastTimeUsed;

    protected virtual void Awake()
    {
        lastTimeUsed -= cooldown;
    }

    public void SetSkillUpgradeType(SkillUpgadeType upgradeType)
    {
        skillUpgadeType = upgradeType;
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
