using UnityEngine;

public class Entity_Stats : MonoBehaviour
{
    public Stat_ResourceGroup resources;
    public Stat_MajorGroup majorStats;
    public Stat_OffenseGroup offenseStats;
    public Stat_DefenseGroup defenseStats;

    public float GetPhysicalDamage(out bool isCritical, float scaleFactor = 1)
    {
        float baseDamage = offenseStats.damage.GetValue();
        float bonusDamage = majorStats.strength.GetValue();
        float totalBaseDamage = baseDamage + bonusDamage;

        float baseCriticalChance = offenseStats.criticalChance.GetValue();
        float bonusCriticalChance = majorStats.agility.GetValue() * 0.3f;
        float criticalChance = baseCriticalChance + bonusCriticalChance;

        float baseCriticalPower = offenseStats.criticalPower.GetValue();
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

        return finalDamage * scaleFactor;
    }

    public float GetElementalDamage(out ElementType element, float scaleFactor = 1)
    {
        float fireDamage = offenseStats.fireDamage.GetValue();
        float iceDamage = offenseStats.iceDamage.GetValue();
        float lightningDamage = offenseStats.lightningDamage.GetValue();
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
            element = ElementType.None;
            return 0;
        }

        float bonusFireDamage = (fireDamage == highestDamage) ?  0 : fireDamage * 0.5f;
        float bonusIceDamage = (iceDamage == highestDamage) ? 0 : iceDamage * 0.5f;
        float bonusLightningDamage = (lightningDamage == highestDamage) ? 0 : lightningDamage * 0.5f;

        float weakerElementalDamage = bonusFireDamage + bonusIceDamage + bonusLightningDamage;

        float finalDamage = highestDamage + weakerElementalDamage + bonusElementalDamage;

        return finalDamage * scaleFactor;
    }

    public float GetElementalResistance(ElementType element)
    {
        float baseResistance = 0;
        float bonusResistance = majorStats.intelligence.GetValue() * 0.5f;

        switch (element)
        {
            case ElementType.Fire:
                baseResistance = defenseStats.fireResistance.GetValue();
                break;
            case ElementType.Ice:
                baseResistance = defenseStats.iceResistance.GetValue();
                break;
            case ElementType.Lightning:
                baseResistance = defenseStats.lightningResistance.GetValue();
                break;
        }

        float totalResistance = baseResistance + bonusResistance;

        float resistanceCap = 0.75f;

        float finalResistance = Mathf.Clamp(totalResistance, 0, resistanceCap);

        return finalResistance;
    }

    public float GetMaxHealth()
    {
        float baseMaxHealth = resources.maxHealth.GetValue();
        float bonusMaxHealth = majorStats.vitality.GetValue() * 5f;

        float finalMaxHealth = baseMaxHealth + bonusMaxHealth;

        return finalMaxHealth;
    }

    public float GetMitigation(float armorReduction)
    {
        float baseArmor = defenseStats.armor.GetValue();
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
        float finalReduction = offenseStats.armorReduction.GetValue() / 100;

        return finalReduction;
    }

    public float GetEvasion()
    {
        float baseEvasion = defenseStats.evasion.GetValue();
        float bonusEvasion = majorStats.agility.GetValue() * 0.5f;

        float totalEvasion = baseEvasion + bonusEvasion;

        float evasionCap = 0.85f;

        float finalEvasion = Mathf.Clamp(totalEvasion, 0, evasionCap);

        return finalEvasion;
    }

    public Stat GetStatByType(Stat_Type statType)
    {
        switch (statType)
        {
            case Stat_Type.MaxHealth:
                return resources.maxHealth;

            case Stat_Type.HealthRegen:
                return resources.healthRegen;

            case Stat_Type.Strength:
                return majorStats.strength;

            case Stat_Type.Intelligence:
                return majorStats.intelligence;

            case Stat_Type.Agility:
                return majorStats.agility;

            case Stat_Type.Vitality:
                return majorStats.vitality;

            case Stat_Type.AttackSpeed:
                return offenseStats.attackSpeed;

            case Stat_Type.Damage:
                return offenseStats.damage;

            case Stat_Type.CriticalChance:
                return offenseStats.criticalChance;

            case Stat_Type.CriticalPower:
                return offenseStats.criticalPower;

            case Stat_Type.FireDamage:
                return offenseStats.fireDamage;

            case Stat_Type.IceDamage:
                return offenseStats.iceDamage;

            case Stat_Type.LightningDamage:
                return offenseStats.lightningDamage;

            case Stat_Type.ArmorReduction:
                return offenseStats.armorReduction;

            case Stat_Type.Armor:
                return defenseStats.armor;

            case Stat_Type.Evasion:
                return defenseStats.evasion;

            case Stat_Type.FireResistance:
                return defenseStats.fireResistance;

            case Stat_Type.IceResistance:
                return defenseStats.iceResistance;

            case Stat_Type.LightningResistance:
                return defenseStats.lightningResistance;

            default:
                Debug.LogWarning("Invalid Stat Type: " + statType);
                return null;
        }
    }
}