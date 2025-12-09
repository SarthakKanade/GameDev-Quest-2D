using UnityEngine;
using System;

[Serializable]
public class UI_TreeConnectionDetails
{
    public UI_TreeConnectionHandler childNode;
    public NodeDirectionType direction;
    [Range(100f, 350f)] public float length;
}

public class UI_TreeConnectionHandler : MonoBehaviour
{
    private RectTransform myRect => GetComponent<RectTransform>();
    [SerializeField] private UI_TreeConnectionDetails[] connectionDetails;
    [SerializeField] private UI_TreeConnection[] connections;

    private void OnValidate()
    {
        if (connectionDetails == null)
        {
            return;
        }

        if (connectionDetails.Length != connections.Length)
        {
            Debug.LogError("Connection details and connections arrays must have the same length." + gameObject.name);
        }

        UpdateConnection();
    }

    private void UpdateConnection()
    {
        for (int i = 0; i < connectionDetails.Length; i++)
        {
            var details = connectionDetails[i];
            var connection = connections[i];
            Vector2 childNodePosition = connection.GetChildNodeConnectionPoint(myRect);

            connection.DirectConection(details.direction, details.length);
            details.childNode?.SetPosition(childNodePosition);
        }
    }

    public void SetPosition(Vector2 position)
    {
        myRect.anchoredPosition = position;
    }
}
