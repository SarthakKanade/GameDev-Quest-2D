using UnityEngine;
using System;

public class Entity_Stats : MonoBehaviour
{
    public Stat maxHP;
    public Stat_MajorGroup majorStats;
    public Stat_OffenseGroup offenseGroup;
    public Stat_DefenseGroup defenseGroup;

    public float GetMaxHealth()
    {
        float baseHP = maxHP.GetValue();
        float bonusHP = majorStats.vitality.GetValue() * 5f;

        return baseHP + bonusHP;
    }
}