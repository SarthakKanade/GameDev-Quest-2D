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

    public float GetElementalDamage(out ElementType element)
    {
        float fireDamage = offenseGroup.fireDamage.GetValue();
        float iceDamage = offenseGroup.iceDamage.GetValue();
        float lightningDamage = offenseGroup.lightningDamage.GetValue();
        float bonusElementalDamage = majorStats.intelligence.GetValue();

        float highestDamage = fireDamage;
        element = ElementType.Fire;

        if (iceDamage > highestDamage)
        {
            highestDamage = iceDamage;
            element = ElementType.Ice;
        }

        if (lightningDamage > highestDamage)
        {
            highestDamage = lightningDamage;
            element = ElementType.Lightning;
        }

        if (highestDamage <= 0)
        {
            return 0;
            element = ElementType.None;
        }

        float bonusFireDamage = (fireDamage == highestDamage) ?  0 : fireDamage * 0.5f;
        float bonusIceDamage = (iceDamage == highestDamage) ? 0 : iceDamage * 0.5f;
        float bonusLightningDamage = (lightningDamage == highestDamage) ? 0 : lightningDamage * 0.5f;

        float weakerElementalDamage = bonusFireDamage + bonusIceDamage + bonusLightningDamage;

        float finalDamage = highestDamage + weakerElementalDamage + bonusElementalDamage;

        return finalDamage;
    }

    public float GetElementalResistance(ElementType element)
    {
        float baseResistance = 0;
        float bonusResistance = majorStats.intelligence.GetValue() * 0.5f;

        switch (element)
        {
            case ElementType.Fire:
                baseResistance = defenseGroup.fireResistance.GetValue();
                break;
            case ElementType.Ice:
                baseResistance = defenseGroup.iceResistance.GetValue();
                break;
            case ElementType.Lightning:
                baseResistance = defenseGroup.lightningResistance.GetValue();
                break;
        }

        float totalResistance = baseResistance + bonusResistance;

        float resistanceCap = 0.75f;

        float finalResistance = Mathf.Clamp(totalResistance, 0, resistanceCap);

        return finalResistance;
    }

    public float GetMaxHealth()
    {
        float baseMaxHealth = maxHealth.GetValue();
        float bonusMaxHealth = majorStats.vitality.GetValue() * 5f;

        float finalMaxHealth = baseMaxHealth + bonusMaxHealth;

        return finalMaxHealth;
    }

    public float GetMitigation(float armorReduction)
    {
        float baseArmor = defenseGroup.armor.GetValue();
        float bonusArmor = majorStats.vitality.GetValue() * 0.5f;

        float totalArmor = baseArmor + bonusArmor;

        float reductionMultiplier = Mathf.Clamp(1 - armorReduction, 0, 1);
        float effectiveArmor = totalArmor * reductionMultiplier;

        float mitigation = effectiveArmor / (effectiveArmor + 100);

        float mitigationCap = 0.85f;

        float finalMitigation = Mathf.Clamp(mitigation, 0, mitigationCap);

        return finalMitigation;
    }

    public float GetArmorReduction()
    {
        float finalReduction = offenseGroup.armorReduction.GetValue() / 100;

        return finalReduction;
    }

    public float GetEvasion()
    {
        float baseEvasion = defenseGroup.evasion.GetValue();
        float bonusEvasion = majorStats.agility.GetValue() * 0.5f;

        float totalEvasion = baseEvasion + bonusEvasion;

        float evasionCap = 0.85f;

        float finalEvasion = Mathf.Clamp(totalEvasion, 0, evasionCap);

        return finalEvasion;
    }
}