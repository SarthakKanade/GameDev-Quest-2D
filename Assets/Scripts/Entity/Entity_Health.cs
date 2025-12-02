using UnityEngine;
using UnityEngine.UI;


public class Entity_Health : MonoBehaviour, IDamagable
{
    private Slider healthBar;
    private Entity_VFX entityVFX;
    private Entity entity;
    private Entity_Stats stats;

    [SerializeField] protected float currentHP;
    [SerializeField] protected bool isDead;

    [Header("On Damage Knockback")] 
    [SerializeField] protected Vector2 knockbackForce = new Vector2(1.5f, 2.5f);
    [SerializeField] protected Vector2 heavyKnockbackForce = new Vector2(7.0f, 7.0f);
    [SerializeField] protected float knockbackDuration = 0.2f;
    [SerializeField] protected float heavyKnockbackDuration = 0.5f;
    
    [Header("On Heavy Damage")]
    [SerializeField] private float heavyDamageThreshold = 0.3f;

    protected virtual void Awake()
    {
        entityVFX = GetComponent<Entity_VFX>();
        entity = GetComponent<Entity>();
        healthBar = GetComponentInChildren<Slider>();
        stats = GetComponent<Entity_Stats>();

        currentHP = stats.GetMaxHealth();
        UpdateHealthBar();
    }
    
    private void UpdateHealthBar()
    {
        if (healthBar != null)
        {
            healthBar.value = currentHP / stats.GetMaxHealth();
        }
    }

    public virtual bool TakeDamage(float damage, float elementDamage, Transform damageDealer)
    {
        if (isDead)
        {
            return false;
        }
        
        if (AttackEavaded())
        {
            Debug.Log($"{gameObject.name} Eavaded the attack");
            return false;
        }

        Entity_Stats attackerStats = damageDealer.GetComponent<Entity_Stats>();

        float armorReduction = 0;
        if(attackerStats != null)
        {
            armorReduction = attackerStats.GetArmorReduction();
        }

        float mitigation = stats.GetMitigation(armorReduction);
        float finalDamage = damage * (1 - mitigation);
        
        Vector2 knockback = CalculateKnockback(finalDamage, damageDealer.position);
        float duration = CalculateDuration(finalDamage);

        entity?.ReceiveKnockback(knockback, duration);
        entityVFX?.PlayOnDamageVFX();
        ReduceHP(finalDamage);  

        return true;
    }

    private bool AttackEavaded()
    {
        return Random.Range(0, 100) < stats.GetEvasion();
    }

    private void ReduceHP(float damage)
    {
        currentHP -= damage;
        UpdateHealthBar();

        if (currentHP <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        isDead = true;
        entity.EntityDeath();
    }

    private Vector2 CalculateKnockback(float damage, Vector2 damageDealerPosition)
    {
        int direction = 1;
        Vector2 knockback;
        if (transform.position.x > damageDealerPosition.x)
        {
            direction = 1;
        }
        else
        {
            direction = -1;
        }

        if (IsHeavyDamage(damage))
        {
            knockback = heavyKnockbackForce;
        }
        else
        {
            knockback = knockbackForce;
        }

        knockback.x *= direction;

        return knockback;
    }

    private float CalculateDuration(float damage)
    {
        return IsHeavyDamage(damage) ? heavyKnockbackDuration : knockbackDuration;
    }

    private bool IsHeavyDamage(float damage)
    {
        if(damage/stats.GetMaxHealth() > heavyDamageThreshold)
        {
            return true;
        }

        return false;
    }
}