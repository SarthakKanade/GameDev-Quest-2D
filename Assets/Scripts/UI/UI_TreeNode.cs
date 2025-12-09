using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_TreeNode : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    private UI ui;
    private RectTransform rectT;
    private UI_SkillTree skillTree;

    [Header("Unlock details")]
    public UI_TreeNode[] neededNodes;
    public UI_TreeNode[] conflictNodes;
    public bool isLocked;
    public bool isUnlocked;

    [Header("Skill details")]
    public Skill_DataSO skillData;
    [SerializeField] private string skillName;
    [SerializeField] private float skillCost;
    [SerializeField] private Image skillIcon;
    [SerializeField] private string lockedColorHex = "#7B7B7B";
    private Color lastIconColor;

    private void Awake()
    {
        ui = GetComponentInParent<UI>();
        rectT = GetComponent<RectTransform>();
        skillTree = GetComponentInParent<UI_SkillTree>();
        UpdateIconColor(UpdateHexColor(lockedColorHex));
    }

    private void Unlock()
    {
        isUnlocked = true;
        UpdateIconColor(Color.white);
        skillTree.RemoveSkillPoint(skillData.cost);
        LockConflictNodes();
    }

    private bool CanBeUnlocked()
    {
        if (isLocked || isUnlocked)
        {
            return false;
        }

        if (!skillTree.EnoughtSkillPoints(skillData.cost))
        {
            return false;
        }

        foreach (var node in neededNodes)
        {
            if (!node.isUnlocked)
            {
                return false;
            }
        }

        foreach (var node in conflictNodes)
        {
            if (node.isUnlocked)
            {
                return false;
            }
        }

        return true;
    }

    private void LockConflictNodes()
    {
        foreach (var node in conflictNodes)
        {
            node.isLocked = true;
        }
    }

    private void UpdateIconColor(Color color)
    {
        if (skillIcon == null)
        {
            return;
        }
        
        lastIconColor = skillIcon.color;
        skillIcon.color = color;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!isUnlocked)
        {
            UpdateIconColor(Color.white * 0.8f);
        }

        ui.skillToolTip.ShowToolTip(true, rectT, this);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!isUnlocked)
        {
            UpdateIconColor(lastIconColor);
        }

        ui.skillToolTip.ShowToolTip(false, rectT);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (CanBeUnlocked())
        {
            Unlock();
        }
    }

    private Color UpdateHexColor(string hexColor)
    {
        ColorUtility.TryParseHtmlString(hexColor, out Color color);
        return color;
    }

    private void OnValidate()
    {
        if (skillData == null)
        {
            return;
        }

        skillName = skillData.skillName;
        skillCost = skillData.cost;
        skillIcon.sprite = skillData.Icon;
        gameObject.name = "UI_TreeNode - " + skillData.skillName;
    }
}
