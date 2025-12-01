using UnityEngine;

public class Entity_Stats : MonoBehaviour
{
    private Stat maxHP;
    public Stat Vitality;

    public float GetMaxHealth()
    {
        float baseHP = maxHP.GetBalue();
        float bonusHP = Vitality.GetBalue() * 5f;

        return baseHP + bonusHP;
    }
}