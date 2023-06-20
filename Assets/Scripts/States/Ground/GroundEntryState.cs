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
            duration = 1.1f / GameObject.Find("BeatManager").GetComponent<BeatManager>().multiplier;
            animator.speed = 0.3f * GameObject.Find("BeatManager").GetComponent<BeatManager>().multiplier;
            animator.SetTrigger("GroundAttack" + attackIndex);
            //Debug.Log("Player Attack " + attackIndex + " Fired!");
        }
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        if (shouldCombo)
        {
            stateMachine.SetNextState(new GroundComboState());
        }

        if (fixedtime >= duration)
        {
            animator.speed = 1;
            stateMachine.SetNextStateToMain();
        }
    }
}
