using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundEntryState : MeleeBaseState
{
    public override void OnEnter(StateMachine _stateMachine)
    {
        base.OnEnter(_stateMachine);

        //Attack
        if (body.gameObject.GetComponent<Ground>().GetOnGround() == false)
        {
            stateMachine.SetNextStateToMain();
            duration = 0f;
        }
        else
        {
            attackIndex = 1;
            duration = 0.5f;
            animator.SetTrigger("Attack" + attackIndex);
            Debug.Log("Player Attack " + attackIndex + " Fired!");
            body.AddForce(new Vector3(body.gameObject.GetComponent<Transform>().localScale.x, 0f, 0f) * 6f, ForceMode2D.Impulse);
        }
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        if (fixedtime >= duration)
        {
            if (shouldCombo)
            {
                stateMachine.SetNextState(new GroundComboState());
            }
            else if (fixedtime >= duration + 0.25f)
            {
                stateMachine.SetNextStateToMain();
            }
        }
    }
}
