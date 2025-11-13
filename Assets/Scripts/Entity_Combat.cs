using System;
using UnityEngine;

public class Entity_Combat : MonoBehaviour
{

    public float damage = 10;

    [Header("Target Detection")]
    [SerializeField] private float targetCheckRadius = 1;
    [SerializeField] private Transform targetCheckPoint;
    [SerializeField] private LayerMask whatIsTarget;

    public void PerformAttack()
    {
        foreach (var target in GetDetectedColliders())
        {
            target.GetComponent<Entity_Health>()?.TakeDamage(damage, transform);
        }
    }

    private Collider2D[] GetDetectedColliders()
    {
        return Physics2D.OverlapCircleAll(targetCheckPoint.position, targetCheckRadius, whatIsTarget);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(targetCheckPoint.position, targetCheckRadius);
    }


}
