using NUnit.Framework;
using Unity.VisualScripting;
using UnityEngine;

public class Player_CounterAttackState : PlayerState
{
    private bool CounteredSomebody;
    private Player_Combat playerCombat;
    public Player_CounterAttackState(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
        playerCombat = player.GetComponent<Player_Combat>();
    }

    public override void Enter()
    {
        base.Enter();
        stateTimer = playerCombat.GetCounterRecoveryDuration();
        CounteredSomebody = playerCombat.CounterAttackPerformed();
        anim.SetBool("counterAttackPerformed", CounteredSomebody);

    }

    public override void Update()
    {
        base.Update();
        player.SetVelocity(0, rb.linearVelocity.y);

        if (triggerCalled)
        {
            stateMachine.ChangeState(player.idleState);
        }

        if (stateTimer < 0 || CounteredSomebody == false)
        {
            stateMachine.ChangeState(player.idleState);
        }
    }
}
