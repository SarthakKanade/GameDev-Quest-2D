using UnityEngine;
using TMPro;
using System.Text;

public class UI_SkillToolTip : UI_ToolTip
{
    [SerializeField] private TextMeshProUGUI skillName;
    [SerializeField] private TextMeshProUGUI skillDescription;
    [SerializeField] private TextMeshProUGUI skillRequirements;

    [Space]
    [SerializeField] private string metConditionHex = "#32CD32";
    [SerializeField] private string notMetConditionHex = "#FF4444";
    [SerializeField] private string importantInfoHex = "#FFD700";

    [SerializeField] private string lockedSkillText = "Fate has chosen otherwise — this skill is beyond your reach.";

    private UI_SkillTree skillTree;

    protected override void Awake()
    {
        base.Awake();
        skillTree = GetComponentInParent<UI_SkillTree>();
    }

    public override void ShowToolTip(bool show, RectTransform targetRectT)
    {
        base.ShowToolTip(show, targetRectT);
    }

    public void ShowToolTip(bool show, RectTransform targetRectT, UI_TreeNode node)
    {
        base.ShowToolTip(show, targetRectT);

        if (!show)
        {
            return;
        }

        skillName.text = node.skillData.skillName;
        skillDescription.text = node.skillData.skillDescription;

        string skillLockedText = $"<color={importantInfoHex}>{lockedSkillText}</color>";
        string requirements = node.isLocked ? skillLockedText : GetRequirements(node.skillData.cost, node.neededNodes, node.conflictNodes);

        skillRequirements.text = requirements;
    }

    private string GetRequirements(int skillCost, UI_TreeNode[] neededNodes, UI_TreeNode[] conflictNodes)
    {
        StringBuilder requirements = new StringBuilder();

        requirements.AppendLine("Requirements:");
        string costColor = skillTree.EnoughtSkillPoints(skillCost) ? metConditionHex : notMetConditionHex;
        requirements.AppendLine($"<color={costColor}> - {skillCost} Skill Points</color>");

        foreach (var node in neededNodes)
        {
            string nodeColor = node.isUnlocked ? metConditionHex : notMetConditionHex;
            requirements.AppendLine($"<color={nodeColor}> - {node.skillData.skillName}</color>");
        }

        if (conflictNodes.Length > 0)
        {
            requirements.AppendLine();
            requirements.AppendLine("<color=" + importantInfoHex + ">Locks Out:</color>");
            foreach (var node in conflictNodes)
            {
                string nodeColor = node.isUnlocked ? metConditionHex : notMetConditionHex;
                requirements.AppendLine($"<color={nodeColor}> - {node.skillData.skillName}</color>");
            }
        }

        return requirements.ToString();

    }
      
}
