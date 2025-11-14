using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class Entity_Health : MonoBehaviour
{
    private Enity_VFX entityVFX;

    [SerializeField] protected float maxHP = 100;
    [SerializeField] protected bool isDead;

    protected virtual void Awake()
    {
        entityVFX = GetComponent<Enity_VFX>();
    }

    public virtual void TakeDamage(float damage, Transform damageDealer)
    {
        if (isDead)
        {
            return;
        }

        entityVFX?.PlayOnDamageVFX();
        
        ReduceHP(damage);
    }

    private void ReduceHP(float damage)
    {
        maxHP -= damage;

        if (maxHP <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        isDead = true;
        Debug.Log("DEAD");
    }
}
