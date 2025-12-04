using System;
using UnityEngine;

public class Entity_Combat : MonoBehaviour
{
    private Entity_VFX vfx;
    private Entity_Stats stats;

    [Header("Target Detection")]
    [SerializeField] private float targetCheckRadius = 1;
    [SerializeField] private Transform targetCheckPoint;
    [SerializeField] private LayerMask whatIsTarget;

    [Header("Status Effects")]
    [SerializeField] private float defaultDuration = 3;
    [SerializeField] private float chilledSlowDownMultiplier = 0.2f;

    private void Awake()
    {
        vfx = GetComponent<Entity_VFX>();
        stats = GetComponent<Entity_Stats>();
    }

    public void PerformAttack()
    {
        foreach (var target in GetDetectedColliders())
        {
            IDamagable damagable = target.GetComponent<IDamagable>();

            if (damagable == null)
            {
                continue;
            }

            bool isCritical;
            float damage = stats.GetPhysicalDamage(out isCritical);

            float elementDamage = stats.GetElementalDamage(out ElementType element, 0.6f);
            
            bool targetGotHit = damagable.TakeDamage(damage, elementDamage, element, transform);

            if (element != ElementType.None)
            {
                ApplyStatusEffect(element, target.transform);
            }

            if (targetGotHit)
            {
                vfx.UpdateOnHitVFXColor(element);
                vfx.CreateOnHitVFX(target.transform, isCritical);
            }
        }
    }

    public void ApplyStatusEffect(ElementType element, Transform target, float scaleFactor = 1)
    {
        Entity_StatusHandler statusHandler = target.GetComponent<Entity_StatusHandler>();

        if (statusHandler == null)
        {
            return;
        }

        if (element == ElementType.Ice && statusHandler.CanBeApplied(ElementType.Ice))
        {
            statusHandler.ApplyChilledStatusEffect(defaultDuration, chilledSlowDownMultiplier * scaleFactor);
        }

        if (element == ElementType.Fire && statusHandler.CanBeApplied(ElementType.Fire))
        {
            float fireDamage = stats.offenseGroup.fireDamage.GetValue() * scaleFactor;
            statusHandler.ApplyBurnedStatusEffect(defaultDuration, fireDamage);
        }
    }

    protected Collider2D[] GetDetectedColliders()
    {
        return Physics2D.OverlapCircleAll(targetCheckPoint.position, targetCheckRadius, whatIsTarget);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(targetCheckPoint.position, targetCheckRadius);
    }


}
