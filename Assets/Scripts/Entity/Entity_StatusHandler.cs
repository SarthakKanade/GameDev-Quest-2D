using UnityEngine;
using System.Collections;


public class Entity_StatusHandler : MonoBehaviour
{
    private Entity entity;
    private Entity_VFX entityVFX;
    private Entity_Stats entityStats;
    private Entity_Health entityHealth;

    [Header("Lightning Strike Details")]
    [SerializeField] private GameObject lightningStrikeVFX;
    [SerializeField] private float currentcharge;
    [SerializeField] private float maxCharge = 1;
    private Coroutine electrifyCo;

    private ElementType currentEffect = ElementType.None;

    protected virtual void Awake()
    {
        entity = GetComponent<Entity>();
        entityVFX = GetComponent<Entity_VFX>();
        entityStats = GetComponent<Entity_Stats>();
        entityHealth = GetComponent<Entity_Health>();
    }

    public bool CanBeApplied(ElementType element)
    {
        if (element == ElementType.Lightning && currentEffect == ElementType.Lightning)
        {
            return true;
        }

        return currentEffect == ElementType.None;
    }

    public void ApplyLightningStatusEffect(float duration, float damage, float chargeAmount)
    {
        float lightningResistane = entityStats.GetElementalResistance(ElementType.Lightning);
        float finalChargeAmount = chargeAmount * (1 - lightningResistane);
        currentcharge += finalChargeAmount;

        if (currentcharge >= maxCharge)
        {
            DoLightningStrike(damage);
            StopElectricEffect();
            return;
        }

        if (electrifyCo != null)
        {
            StopCoroutine(electrifyCo);
        }
        
        electrifyCo = StartCoroutine(ElectrifyEffectCo(duration));
    }

    private void DoLightningStrike(float damage)
    {
        Instantiate(lightningStrikeVFX, transform.position, Quaternion.identity);
        entityHealth.ReduceHP(damage);
    }

    private void StopElectricEffect()
    {
        currentEffect = ElementType.None;
        currentcharge = 0;
        entityVFX.StopAllVFX();
    }

    private IEnumerator ElectrifyEffectCo(float duration)
    {
        currentEffect = ElementType.Lightning;
        entityVFX.PlayOnStatusEffectVFX(duration, ElementType.Lightning);

        yield return new WaitForSeconds(duration);

        StopElectricEffect();
    }



    public void ApplyChillStatusEffect(float duration, float slowDownMultiplier)
    {
        float iceResistance = entityStats.GetElementalResistance(ElementType.Ice);
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

    public void ApplyBurnStatusEffect(float duration, float totalDamage)
    {
        float fireResistance = entityStats.GetElementalResistance(ElementType.Fire);
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
