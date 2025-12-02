using UnityEngine;

public class Enemy_Health : Entity_Health
{
    private Enemy enemy => GetComponent<Enemy>();
    public override bool TakeDamage(float damage, float elementDamage, Transform damageDealer)
    {
        bool wasHit = base.TakeDamage(damage, elementDamage, damageDealer);

        if (!wasHit)
        {
            return false;
        }

        if (damageDealer.GetComponent<Player>() != null)
        {
            enemy.TryEnterBattleState(damageDealer);
        }
        
        return true;
    }
}
