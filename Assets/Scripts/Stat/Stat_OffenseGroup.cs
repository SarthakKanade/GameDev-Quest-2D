using UnityEngine;
using System;

[Serializable]
public class Stat_OffenseGroup
{
    public Stat attackSpeed;
    //Physical damage
    public Stat damage;
    public Stat criticalPower;
    public Stat criticalChance;
    public Stat armorReduction;

    //Elemental damage
    public Stat fireDamage;
    public Stat iceDamage;
    public Stat lightningDamage;
}
 