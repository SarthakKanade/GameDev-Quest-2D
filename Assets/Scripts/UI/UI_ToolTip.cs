using UnityEngine;

public class UI_ToolTip : MonoBehaviour
{
    private RectTransform rectT;
    // private float xOffset = 300;
    // private float yOffset = 20;
    [SerializeField] private Vector2 offset = new Vector2(300, 20);

    protected virtual void Awake()
    {
        rectT = GetComponent<RectTransform>();
    }

    public virtual void ShowToolTip(bool show, RectTransform targetRectT)
    {
        if (!show)
        {
            rectT.position = new Vector3(10000, 10000, 10000);
            return;
        }

        UpdatePosition(targetRectT);
    }

    private void UpdatePosition(RectTransform targetRectT)
    {
        float screenCenterX = Screen.width / 2;
        float screenTop = Screen.height;
        float screenBottom = 0;

        Vector2 targetPosition = targetRectT.position;
        if (targetPosition.x < screenCenterX)
        {
            targetPosition.x += offset.x;
        }
        else
        {
            targetPosition.x -= offset.x;
        }

        float rectVerticalHalf = (rectT.sizeDelta.y / 2);
        float rectTopY = targetPosition.y + rectVerticalHalf;
        float rectBottomY = targetPosition.y - rectVerticalHalf;

        if (rectTopY > screenTop)
        {
            targetPosition.y = screenTop - rectVerticalHalf - offset.y;
        }
        else if (rectBottomY < screenBottom)
        {
            targetPosition.y = screenBottom + rectVerticalHalf + offset.y;
        }

        rectT.position = targetPosition;
    }
}
