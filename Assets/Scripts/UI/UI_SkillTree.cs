using UnityEngine;

public class UI_SkillTree : MonoBehaviour
{
    public int skillPoints;

    public bool EnoughtSkillPoints(int cost)
    {
        return skillPoints >= cost;
    }

    public void RemoveSkillPoint(int cost)
    {
        skillPoints -= cost;
    }
}
