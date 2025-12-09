using UnityEngine;
using TMPro;

public class UI_SkillToolTip : UI_ToolTip
{
    [SerializeField] private TextMeshProUGUI skillName;
    [SerializeField] private TextMeshProUGUI skillDescription;
    [SerializeField] private TextMeshProUGUI skillRequirements;

    public override void ShowToolTip(bool show, RectTransform targetRectT)
    {
        base.ShowToolTip(show, targetRectT);
    }

    public void ShowToolTip(bool show, RectTransform targetRectT, Skill_DataSO skillData)
    {
        base.ShowToolTip(show, targetRectT);

        if (!show)
        {
            return;
        }

        UpdateToolTip(skillData);
    }

    private void UpdateToolTip(Skill_DataSO skillData)
    {
        skillName.text = skillData.skillName;
        skillDescription.text = skillData.skillDescription;
        skillRequirements.text = "Requirements: \n" + " -" + skillData.cost + " Skill Points";
    }
}
