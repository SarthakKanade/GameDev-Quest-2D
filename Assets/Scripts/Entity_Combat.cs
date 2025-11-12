using System;
using UnityEngine;

public class Entity_Combat : MonoBehaviour
{

    [Header("Target Detection")]
    [SerializeField] private float targetCheckRadius = 1;
    [SerializeField] private Transform targetCheckPoint;
    [SerializeField] private LayerMask whatIsTarget;

    public void PerformAttack()
    {
        foreach (var target in GetDetectedColliders())
        {
            Debug.Log(target.name);
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
