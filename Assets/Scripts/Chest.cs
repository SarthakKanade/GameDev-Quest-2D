using UnityEngine;

public class Chest : MonoBehaviour, IDamagable
{
    private Animator anim => GetComponentInChildren<Animator>();
    private Rigidbody2D rb => GetComponent<Rigidbody2D>();
    private Entity_VFX fx => GetComponent<Entity_VFX>();

    [Header("Chest Open Details")]
    [SerializeField] private Vector2 knockback;
    
    public bool TakeDamage(float damage, float elementDamage, Transform damageDealer)
    {
        fx.PlayOnDamageVFX();
        anim.SetBool("open", true);
        rb.linearVelocity = knockback;
        rb.angularVelocity = Random.Range(-200, 200);
        
        return true;
    }
}
