using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class Entity_Health : MonoBehaviour
{
    [SerializeField] protected float maxHP = 100;
    [SerializeField] protected bool isDead;

    public virtual void TakeDamage(float damage, Transform damageDealer)
    {
        if (isDead)
        {
            return;
        }
        
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
