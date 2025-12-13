using UnityEngine;

public class SkillObject_Base : MonoBehaviour
{
    [SerializeField] protected LayerMask whatIsEnemy;
    [SerializeField] protected Transform targetCheck;
    [SerializeField] protected float explosiveRadius;

    protected void DamageEnemiesInRadius(Transform t, float radius)
    {
        foreach (var target in EnemiesAround(t, radius))
        {
            IDamagable damageable = target.GetComponent<IDamagable>();
            if (damageable != null)
            {
                damageable.TakeDamage(1, 1, ElementType.None, transform);
            }
        }
    }

    protected Collider2D[] EnemiesAround(Transform t, float radius)
    {
        return Physics2D.OverlapCircleAll(t.position, radius, whatIsEnemy);
    }

    protected virtual void OnDrawGizmos()
    {
        if (targetCheck == null)
        {
            targetCheck = transform;
        }

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(targetCheck.position, explosiveRadius);
    }
}
