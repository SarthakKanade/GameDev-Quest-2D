using UnityEngine;
using System.Collections;

public class Entity_StatusHandler : MonoBehaviour
{
    private Entity entity;
    private Entity_VFX entityVFX;
    private Entity_Stats stats;

    private ElementType currentEffect = ElementType.None;

    protected virtual void Awake()
    {
        entity = GetComponent<Entity>();
        entityVFX = GetComponent<Entity_VFX>();
        stats = GetComponent<Entity_Stats>();
    }

    public bool CanBeApplied(ElementType element)
    {
        return currentEffect == ElementType.None;
    }

    public void ApplyChilledStatusEffect(float duration, float slowDownMultiplier)
    {
        float iceResistance = stats.GetElementalResistance(ElementType.Ice);
        float reducedDuration = duration * (1 - iceResistance);

        StartCoroutine(ChilledEffectCo(reducedDuration, slowDownMultiplier));
    }

    public IEnumerator ChilledEffectCo(float duration, float slowDownMultiplier)
    {
        entity.SlowDownEntity(duration, slowDownMultiplier);
        currentEffect = ElementType.Ice;
        entityVFX.PlayOnStatusEffectVFX(duration, ElementType.Ice);

        yield return new WaitForSeconds(duration);

        currentEffect = ElementType.None;
    }
}
