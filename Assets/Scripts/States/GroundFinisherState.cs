using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundFinisherState : MeleeBaseState
{
    public override void OnEnter(StateMachine _stateMachine)
    {
        base.OnEnter(_stateMachine);

        attackIndex = 3;
        duration = 0.5f;
        animator.SetTrigger("attack" + attackIndex);
        Debug.Log("Player Attack " + attackIndex + " Fired!");
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        if (fixedtime >= duration)
        {
            animator.ResetTrigger("attack" + attackIndex);
            stateMachine.SetNextStateToMain();
        }
    }
}