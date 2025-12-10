using UnityEngine;

public class Player_SkillManager : MonoBehaviour
{
    public Skill_Dash dash {get; private set;}

    private void Awake()
    {
        dash = GetComponentInChildren<Skill_Dash>();
    }

    public Skill_Base GetSkillByType(SkillType skillType)
    {
        switch (skillType)
        {
            case SkillType.Dash:
                return dash;
            default:
                Debug.LogError($"Skill Type not found: {skillType}");
                return null;
        }
    }
}
