using UnityEngine;

public class Entity_Stats : MonoBehaviour
{
    public Stat maxHealth;
    public Stat_MajorGroup majorStats;
    public Stat_OffenseGroup offenseGroup;
    public Stat_DefenseGroup defenseGroup;

    public float GetPhysicalDamage(out bool isCritical)
    {
        float baseDamage = offenseGroup.damage.GetValue();
        float bonusDamage = majorStats.strength.GetValue();
        float totalBaseDamage = baseDamage + bonusDamage;

        float baseCriticalChance = offenseGroup.criticalChance.GetValue();
        float bonusCriticalChance = majorStats.agility.GetValue() * 0.3f;
        float criticalChance = baseCriticalChance + bonusCriticalChance;

        float baseCriticalPower = offenseGroup.criticalPower.GetValue();
        float bonusCriticalPower = majorStats.strength.GetValue() * 0.5f;
        float criticalPower = baseCriticalPower + bonusCriticalPower;

        isCritical = Random.Range(0, 100) < criticalChance;

        float finalDamage;

        if (isCritical)
        {
            finalDamage = totalBaseDamage * criticalPower;
        }
        else
        {
            finalDamage = totalBaseDamage;
        }

        return finalDamage;
    }

    public float GetMaxHealth()
    {
        float baseMaxHealth = maxHealth.GetValue();
        float bonusMaxHealth = majorStats.vitality.GetValue() * 5f;

        float finalMaxHealth = baseMaxHealth + bonusMaxHealth;

        return finalMaxHealth;
    }

    public float GetEvasion()
    {
        float baseEvasion = defenseGroup.evasion.GetValue();
        float bonusEvasion = majorStats.agility.GetValue() * 0.5f;

        float finalEvasion = baseEvasion + bonusEvasion;

        return finalEvasion;
    }
}