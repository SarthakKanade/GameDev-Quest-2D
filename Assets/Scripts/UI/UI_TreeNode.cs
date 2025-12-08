using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_TreeNode : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    [SerializeField] private Skill_DataSO skillData;
    [SerializeField] private string skillName;
    [SerializeField] private Image skillIcon;
    [SerializeField] private string lockedColorHex = "#7B7B7B";
    private Color lastIconColor;
    public bool isLocked;
    public bool isUnlocked;

    private void OnValidate()
    {
        if (skillData == null)
        {
            return;
        }

        skillName = skillData.skillName;
        skillIcon.sprite = skillData.Icon;
        gameObject.name = "UI_TreeNode - " + skillData.skillName;
    }

    private void Awake()
    {
        UpdateIconColor(UpdateHexColor(lockedColorHex));
    }

    private void Unlock()
    {
        isUnlocked = true;
        UpdateIconColor(Color.white);
    }

    private bool CanBeUnlocked()
    {
        if (isLocked || isUnlocked)
        {
            return false;
        }

        return true;
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
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!isUnlocked)
        {
            UpdateIconColor(lastIconColor);
        }
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
}
