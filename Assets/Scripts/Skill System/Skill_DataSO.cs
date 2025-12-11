using UnityEngine;
using System;


[CreateAssetMenu(fileName = "Skill Data - ", menuName = "RPG Setup/ Skill Data")]
public class Skill_DataSO : ScriptableObject
{
    public int cost;
    public SkillType skillType;
    public SkillUpgradeData upgradeData;

    [Header("Skill Info")]
    public string skillName;
    [TextArea] public string skillDescription;
    public Sprite Icon;
}

[Serializable]
public class SkillUpgradeData
{
    public SkillUpgradeType upgradeType;
    public float cooldown;
}
