using System;
using UnityEngine;

public class Entity_Combat : MonoBehaviour
{
    private Entity_VFX vfx;
    public float damage = 10;

    [Header("Target Detection")]
    [SerializeField] private float targetCheckRadius = 1;
    [SerializeField] private Transform targetCheckPoint;
    [SerializeField] private LayerMask whatIsTarget;

    private void Awake()
    {
        vfx = GetComponent<Entity_VFX>();
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

            bool targetGotHit = damagable.TakeDamage(damage, transform);

            if (targetGotHit)
            {
                vfx.CreateOnHitVFX(target.transform);
            }
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
