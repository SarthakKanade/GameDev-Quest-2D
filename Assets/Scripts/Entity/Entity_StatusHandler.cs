using UnityEngine;
using System.Collections;

public class Entity_StatusHandler : MonoBehaviour
{
    private Entity entity;
    private Entity_VFX entityVFX;
    private Entity_Stats stats;
    private Entity_Health entityHealth;

    private ElementType currentEffect = ElementType.None;

    protected virtual void Awake()
    {
        entity = GetComponent<Entity>();
        entityVFX = GetComponent<Entity_VFX>();
        stats = GetComponent<Entity_Stats>();
        entityHealth = GetComponent<Entity_Health>();
    }

    public bool CanBeApplied(ElementType element)
    {
        return currentEffect == ElementType.None;
    }

    public void ApplyChilledStatusEffect(float duration, float slowDownMultiplier)
    {
        float iceResistance = stats.GetElementalResistance(ElementType.Ice);
        float finalChillDuration = duration * (1 - iceResistance);

        StartCoroutine(ChilledEffectCo(finalChillDuration, slowDownMultiplier));
    }

    private IEnumerator ChilledEffectCo(float duration, float slowDownMultiplier)
    {
        entity.SlowDownEntity(duration, slowDownMultiplier);
        currentEffect = ElementType.Ice;
        entityVFX.PlayOnStatusEffectVFX(duration, ElementType.Ice);

        yield return new WaitForSeconds(duration);

        currentEffect = ElementType.None;
    }

    public void ApplyBurnedStatusEffect(float duration, float totalDamage)
    {
        float fireResistance = stats.GetElementalResistance(ElementType.Fire);
        float finalBurnDamage = totalDamage * (1 - fireResistance);

        StartCoroutine(BurnedVFXCo(duration, finalBurnDamage));
    }   

    private IEnumerator BurnedVFXCo(float duration, float totalDamage)
    {
        currentEffect = ElementType.Fire;
        entityVFX.PlayOnStatusEffectVFX(duration, ElementType.Fire);

        int ticksPerSecond = 1;
        int tickCount = Mathf.RoundToInt(duration * ticksPerSecond);
        
        float damagePerTick = totalDamage / tickCount;
        float tickInterval = 1f / ticksPerSecond;

        for (int i = 0; i < tickCount; i++)
        {
            entityHealth.ReduceHP(damagePerTick);
            yield return new WaitForSeconds(tickInterval);
        }

        currentEffect = ElementType.None;
    }
}
