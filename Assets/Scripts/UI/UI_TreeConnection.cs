using UnityEngine;

public class UI_TreeConnection : MonoBehaviour
{
    [SerializeField] private RectTransform rotationPoint;
    [SerializeField] private RectTransform connectionLength;
    [SerializeField] private RectTransform childNodeConnectionPoint;

    public void DirectConection(NodeDirectionType direction, float Length)
    {
        bool shouldbeActive = direction != NodeDirectionType.None;

        float finalLength = shouldbeActive ? Length : 0;
        float angle = GetDirectionAngle(direction);

        rotationPoint.localRotation = Quaternion.Euler(0, 0, angle);
        connectionLength.sizeDelta = new Vector2(finalLength, connectionLength.sizeDelta.y);
    }

    public Vector2 GetChildNodeConnectionPoint(RectTransform rect)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle
        (
            rect.parent as RectTransform,
            childNodeConnectionPoint.position,
            null,
            out Vector2 localPoint
        );

        return localPoint;
    }
    


    private float GetDirectionAngle(NodeDirectionType direction)
    {
        switch (direction)
        {
            case NodeDirectionType.UpLeft:
                return 135;
            case NodeDirectionType.Up:
                return 90;
            case NodeDirectionType.UpRight:
                return 45;
            case NodeDirectionType.Right:
                return 0;
            case NodeDirectionType.DownRight:
                return -45;
            case NodeDirectionType.Down:
                return -90;
            case NodeDirectionType.DownLeft:
                return -135;
            case NodeDirectionType.Left:
                return 180;
            default:
                return 0;
        }
    }
}

public enum NodeDirectionType
{
    None,
    UpLeft,
    Up,
    UpRight,
    Right,
    DownRight,
    Down,
    DownLeft,
    Left
}
