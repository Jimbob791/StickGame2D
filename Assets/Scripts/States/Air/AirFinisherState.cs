using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirFinisherState : MeleeBaseState
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
            attackIndex = 2;
            stunTime = 1.5f;
            damage = 5;
            knockback = new Vector2(4, 6);
            duration = 1.1f / GameObject.Find("BeatManager").GetComponent<BeatManager>().multiplier;
            animator.speed = 0.3f * GameObject.Find("BeatManager").GetComponent<BeatManager>().multiplier;
            animator.SetTrigger("AirAttack" + attackIndex);
            body.velocity = new Vector3(body.gameObject.GetComponent<Move>().facing * 10f, 3f, 0f);
            GameObject.Find("Slash2Sound").GetComponent<AudioSource>().Play();
        }
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        if (shouldCombo)
        {
            stateMachine.SetNextState(new AirBonusState());
        }

        if (fixedtime >= duration)
        {
            animator.speed = 1;
            body.gravityScale = 1f;
            stateMachine.SetNextStateToMain();
        }
    }
}
