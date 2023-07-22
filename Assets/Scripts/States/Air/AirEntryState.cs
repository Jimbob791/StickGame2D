using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirEntryState : MeleeBaseState
{
    public override void OnEnter(StateMachine _stateMachine)
    {
        base.OnEnter(_stateMachine);

        //Attack
        if (body.gameObject.GetComponent<Ground>().GetOnGround() == true)
        {
            stateMachine.SetNextStateToMain();
            duration = 0f;
        }
        else
        {
            attackIndex = 1;
            stunTime = 0.1f;
            knockback = new Vector2(0, 0);
            duration = 1.1f / GameObject.Find("BeatManager").GetComponent<BeatManager>().multiplier;
            animator.speed = 0.3f * GameObject.Find("BeatManager").GetComponent<BeatManager>().multiplier;
            animator.SetTrigger("AirAttack" + attackIndex);
            body.velocity = new Vector3(body.gameObject.GetComponent<Move>().facing * 10f, 3f, 0f);
        }
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        if (shouldCombo)
        {
            stateMachine.SetNextState(new AirFinisherState());
        }

        if (fixedtime >= duration)
        {
            animator.speed = 1;
            body.gravityScale = 1f;
            stateMachine.SetNextStateToMain();
        }
    }
}
