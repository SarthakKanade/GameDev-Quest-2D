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

            float elementDamage = stats.GetElementalDamage(out ElementType element);
            
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

    public void ApplyStatusEffect(ElementType element, Transform target)
    {
        Entity_StatusHandler statusHandler = target.GetComponent<Entity_StatusHandler>();

        if (statusHandler == null)
        {
            return;
        }

        if (element == ElementType.Ice && statusHandler.CanBeApplied(ElementType.Ice))
        {
            statusHandler.ApplyChilledStatusEffect(defaultDuration, chilledSlowDownMultiplier);
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
