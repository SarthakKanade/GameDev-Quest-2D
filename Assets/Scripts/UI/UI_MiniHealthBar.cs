using UnityEngine;

public class UI_MiniHealthBar : MonoBehaviour
{
    private Entity entity;
    
    private void Awake()
    {
        entity = GetComponentInParent<Entity>();
    }

    private void OnEnable()
    {
        entity.OnFlipped += HandelFlip;
    }

    private void OnDisable()
    {
        entity.OnFlipped -= HandelFlip;
    }

    private void HandelFlip()
    {
        transform.rotation = Quaternion.identity;
    }
}
