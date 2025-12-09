using UnityEngine;
using TMPro;
using System.Text;
using System.Collections;

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


    private UI ui;
    private UI_SkillTree skillTree;

    private Coroutine textEffectCo;

    protected override void Awake()
    {
        base.Awake();
        ui = GetComponentInParent<UI>();
        skillTree = ui.GetComponentInChildren<UI_SkillTree>(true);
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

        string skillLockedText = GetColoredText(importantInfoHex, lockedSkillText);
        string requirements = node.isLocked ? skillLockedText : GetRequirements(node.skillData.cost, node.neededNodes, node.conflictNodes);

        skillRequirements.text = requirements;
    }

    private string GetRequirements(int skillCost, UI_TreeNode[] neededNodes, UI_TreeNode[] conflictNodes)
    {
        StringBuilder requirements = new StringBuilder();
        string costColor = skillTree.EnoughtSkillPoints(skillCost) ? metConditionHex : notMetConditionHex;
        string costText = $"{skillCost} Skill Points";
        string finalCostText = GetColoredText(costColor, costText);

        requirements.AppendLine("Requirements:");
        requirements.AppendLine(finalCostText);

        foreach (var node in neededNodes)
        {
            string nodeColor = node.isUnlocked ? metConditionHex : notMetConditionHex;
            string nodeText = $" - {node.skillData.skillName}";
            requirements.AppendLine(GetColoredText(nodeColor, nodeText));
        }

        if (conflictNodes.Length > 0)
        {
            requirements.AppendLine();
            requirements.AppendLine(GetColoredText(importantInfoHex, "Locks Out:"));
            foreach (var node in conflictNodes)
            {
                string nodeText = $" - {node.skillData.skillName}";
                string finalNodeText = GetColoredText(importantInfoHex, nodeText);
                requirements.AppendLine(finalNodeText);
            }
        }

        return requirements.ToString();
    }

    public void ShowLockedSkillEffect()
    {
        if (textEffectCo != null)
        {
            StopCoroutine(textEffectCo);
        }

        textEffectCo = StartCoroutine(TextBlinkEffect(skillRequirements, 0.2f, 3));
    }

    private IEnumerator TextBlinkEffect(TextMeshProUGUI text, float blinkInterval, int blinkCount)
    {
        for (int i = 0; i < blinkCount; i++)
        {
            text.text = GetColoredText(notMetConditionHex, lockedSkillText);
            yield return new WaitForSeconds(blinkInterval);

            text.text = GetColoredText(importantInfoHex, lockedSkillText);
            yield return new WaitForSeconds(blinkInterval);
        }
    }
}
