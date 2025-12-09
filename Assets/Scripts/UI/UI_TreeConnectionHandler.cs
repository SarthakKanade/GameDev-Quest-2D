using UnityEngine;
using System;
using UnityEngine.UI;

[Serializable]
public class UI_TreeConnectionDetails
{
    public UI_TreeConnectionHandler childNode;
    public NodeDirectionType direction;
    [Range(100f, 350f)] public float length;
    [Range(-45f, 45f)] public float rotation;
}

public class UI_TreeConnectionHandler : MonoBehaviour
{
    private RectTransform myRect => GetComponent<RectTransform>();
    [SerializeField] private UI_TreeConnectionDetails[] connectionDetails;
    [SerializeField] private UI_TreeConnection[] connections;

    private Image connectionImage;
    private Color originalColor;

    public void Awake()
    {
        if (connectionImage != null)
        {
            originalColor = connectionImage.color;
        }
    }

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
            Image connectionImage = connection.GetConnectionImage();

            connection.DirectConection(details.direction, details.length, details.rotation);

            if (details.childNode == null)
            {
                continue;
            }
            
            details.childNode.SetPosition(childNodePosition);
            details.childNode.SetConnectionImage(connectionImage);
            details.childNode.transform.SetAsLastSibling();
        }
    }

    public void UpdateAllConnections()
    {
        UpdateConnection();

        foreach (var node in connectionDetails)
        {
            if (node.childNode == null)
            {
                continue;
            }

            node.childNode.UpdateAllConnections();
        }
    }

    public void UnlockConnectionImage(bool unlocked)    
    {
        if (connectionImage != null)
        {
            if (unlocked)
            {
                connectionImage.color = Color.white;
            }
            else
            { 
                connectionImage.color = originalColor;
            }
        }
    }

    public void SetConnectionImage(Image image)
    {
        connectionImage = image;
    }

    public void SetPosition(Vector2 position)
    {
        myRect.anchoredPosition = position;
    }
}
