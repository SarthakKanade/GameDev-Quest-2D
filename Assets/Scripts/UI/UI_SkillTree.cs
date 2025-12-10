using UnityEngine;

public class UI_SkillTree : MonoBehaviour
{
    [SerializeField] private int skillPoints;
    [SerializeField] private UI_TreeConnectionHandler[] parentNodes;

    public Player_SkillManager skillManager{get; private set;}

    private void Awake()
    {
        skillManager = FindAnyObjectByType<Player_SkillManager>();
    }

    private void Start()
    {
        UpdateAllConnections();
    }

    public void RefundAllSkill()
    {
        UI_TreeNode[] skillNodes = GetComponentsInChildren<UI_TreeNode>();
        foreach (var node in skillNodes)
        {
            node.Refund();
        }
    }

    public bool EnoughtSkillPoints(int cost)
    {
        return skillPoints >= cost;
    }

    public void RemoveSkillPoint(int cost)
    {
        skillPoints -= cost;
    }

    public void AddSkillPoint(int points)
    {
        skillPoints += points;
    }

    [ContextMenu("Update All Connections")]
    public void UpdateAllConnections()
    {
        foreach (var node in parentNodes)
        {
            node.UpdateAllConnections();
        }
    }
}
