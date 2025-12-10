using UnityEngine;


[CreateAssetMenu(fileName = "Skill Data - ", menuName = "RPG Setup/ Skill Data")]
public class Skill_DataSO : ScriptableObject
{
    public int cost;
    public SkillType skillType;
    public SkillUpgadeType skillUpgradeType;

    [Header("Skill Info")]
    public string skillName;
    [TextArea] public string skillDescription;
    public Sprite Icon;
}
