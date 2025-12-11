using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_TreeNode : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    private UI ui;
    private RectTransform rectT;
    private UI_SkillTree skillTree;
    private UI_TreeConnectionHandler connectionHandler;

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
        connectionHandler = GetComponent<UI_TreeConnectionHandler>();
        UpdateIconColor(UpdateHexColor(lockedColorHex));
    }

    public void Refund()
    {
        isUnlocked = false;
        isLocked = false;
        UpdateIconColor(UpdateHexColor(lockedColorHex));
        skillTree.AddSkillPoint(skillData.cost);
        connectionHandler.UnlockConnectionImage(false);
    }

    private void Unlock()
    {
        isUnlocked = true;
        UpdateIconColor(Color.white);
        LockConflictNodes();

        skillTree.RemoveSkillPoint(skillData.cost);
        connectionHandler.UnlockConnectionImage(true);

        skillTree.skillManager.GetSkillByType(skillData.skillType).SetSkillUpgrade(skillData.upgradeData);


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
        ui.skillToolTip.ShowToolTip(true, rectT, this);

        if (isUnlocked || isLocked)
        {
            return;
        }

        ToggleNodeHighlight(true);

    }

    public void OnPointerExit(PointerEventData eventData)
    {
       ui.skillToolTip.ShowToolTip(false, rectT);
        
        if (isUnlocked || isLocked)
        {
            return;
        }

        ToggleNodeHighlight(false);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (CanBeUnlocked())
        {
            Unlock();
        }
        else if (isLocked)
        {
            ui.skillToolTip.ShowLockedSkillEffect();
        }
    }

    private void ToggleNodeHighlight(bool highlight)
    {
        Color highlightColor = Color.white * 0.8f; highlightColor.a = 1;
        Color colorToApply = highlight ? highlightColor : lastIconColor;
        UpdateIconColor(colorToApply);
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

    private void OnDisable()
    {
        if (isLocked)
        {
            UpdateIconColor(UpdateHexColor(lockedColorHex));
        }

        if (isUnlocked)
        {
            UpdateIconColor(Color.white);
        }
    }
}
