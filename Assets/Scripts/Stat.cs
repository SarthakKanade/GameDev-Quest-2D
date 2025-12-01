using UnityEngine;

public class Stat : MonoBehaviour
{
    [SerializeField] private float baseValue;

    public float GetBalue()
    {
        return baseValue;
    }
}
