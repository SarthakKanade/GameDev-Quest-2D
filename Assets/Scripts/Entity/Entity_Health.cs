using UnityEngine;
using UnityEngine.UI;


public class Entity_Health : MonoBehaviour, IDamagable
{
    private Slider healthBar;
    private Entity entity;
    private Entity_VFX entityVFX;
    private Entity_Stats entityStats;

    [SerializeField] protected float currentHealth;
    [SerializeField] protected bool isDead;
    
    [Header("Health Regeneration")]
    [SerializeField] private float regenInterval = 1f;
    [SerializeField] private bool canRegenerateHealth = true;
    

    [Header("On Damage Knockback")] 
    [SerializeField] private Vector2 knockbackForce = new Vector2(1.5f, 2.5f);
    [SerializeField] private Vector2 heavyKnockbackForce = new Vector2(7.0f, 7.0f);
    [SerializeField] private float knockbackDuration = 0.2f;
    [SerializeField] private float heavyKnockbackDuration = 0.5f;
    
    [Header("On Heavy Damage")]
    [SerializeField] private float heavyDamageThreshold = 0.3f;

    protected virtual void Awake()
    {
        healthBar = GetComponentInChildren<Slider>();
        entity = GetComponent<Entity>();
        entityVFX = GetComponent<Entity_VFX>();
        entityStats = GetComponent<Entity_Stats>();

        InvokeRepeating(nameof(RegenerateHealth), 0, regenInterval);
    }

    protected virtual void Start()
    {
        currentHealth = entityStats.GetMaxHealth();
        UpdateHealthBar();
    }
    
    private void UpdateHealthBar()
    {
        if (healthBar != null)
        {
            healthBar.value = currentHealth / entityStats.GetMaxHealth();
        }
    }

    public virtual bool TakeDamage(float damage, float elementDamage, ElementType element, Transform damageDealer)
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

        float mitigation = entityStats.GetMitigation(armorReduction);
        float physicalDamageTaken = damage * (1 - mitigation);

        float elementResistance = entityStats.GetElementalResistance(element);
        float elementDamageTaken = elementDamage * (1 - elementResistance);
        
        TakeKnockback(physicalDamageTaken, damageDealer);
        
        ReduceHealth(physicalDamageTaken + elementDamageTaken);
        return true;
    }

    private bool AttackEavaded()
    {
        return Random.Range(0, 100) < entityStats.GetEvasion();
    }

    private void RegenerateHealth()
    {
        if (!canRegenerateHealth)
        {
            return;
        }

        float regenAmount = entityStats.resources.healthRegen.GetValue();
        IncreaseHealth(regenAmount);
    }

    public void IncreaseHealth(float healAmount)
    {
        if (isDead)
        {
            return;
        }
        
        float newHealth = currentHealth + healAmount;
        float maxHealth = entityStats.GetMaxHealth();

        currentHealth = Mathf.Min(newHealth, maxHealth);
    }

    public void ReduceHealth(float damage)
    {
        entityVFX?.PlayOnDamageVFX();
        currentHealth -= damage;
        UpdateHealthBar();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        isDead = true;
        entity.EntityDeath();
    }

    private void TakeKnockback(float finalDamage, Transform damageDealer)
    {
        Vector2 knockback = CalculateKnockback(finalDamage, damageDealer.position);
        float duration = CalculateDuration(finalDamage);

        entity?.ReceiveKnockback(knockback, duration);
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
        if(damage/entityStats.GetMaxHealth() > heavyDamageThreshold)
        {
            return true;
        }

        return false;
    }
}