using UnityEngine;

public interface IDamagable
{
    public bool TakeDamage(float damage, float elementDamage, ElementType element, Transform damageDealer);
}
