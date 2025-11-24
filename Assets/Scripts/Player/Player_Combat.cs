using UnityEngine;

public class Player_Combat : Entity_Combat
{
    [Header("Counter details")]
    [SerializeField] private float counterRecoveryDuration;
   public bool CounterAttackPerformed()
   {
    bool hasCounteredSomebody = false;

        foreach (var target in GetDetectedColliders())
        {
            ICounterable counterable = target.GetComponent<ICounterable>();

            if (counterable == null)
            {
                continue;
            }

            if (counterable.CanBeCountered)
            {
                counterable.HandleCounter();
                hasCounteredSomebody = true;
            }
        }
        return hasCounteredSomebody;
   }

   public float GetCounterRecoveryDuration()
   {
    return counterRecoveryDuration;
   }
}
