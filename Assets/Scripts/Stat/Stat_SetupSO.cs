using UnityEngine;

[CreateAssetMenu(fileName = "Default Stat Setup", menuName = "RPG Setup/Stat Setup")]
public class Stat_SetupSO : ScriptableObject
{
    [Header("Resources")]
    public float maxHealth = 100;
    public float healthRegen;

    [Header("Major Stats")]
    public float strength;
    public float intelligence;
    public float agility;
    public float vitality;
    
    [Header("Offense - Physical Damage")]
    public float attackSpeed = 1;
    public float damage = 10;
    public float criticalChance;
    public float criticalPower = 150;
    public float armorReduction = 1;

    [Header("Offense - Elemental Damage")]
    public float fireDamage;
    public float iceDamage;
    public float lightningDamage;

    [Header("Defense - Physical Damage")]
    public float evasion;
    public float armor;
    
    [Header("Defense - Elemental Damage")]
    public float fireResistance;
    public float iceResistance;
    public float lightningResistance;

}
