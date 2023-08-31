using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundComboState : MeleeBaseState
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
            attackIndex = 2;
            stunTime = 0.6f;
            damage = 6;
            knockback = new Vector2(1, 1);
            duration = 1.1f / GameObject.Find("BeatManager").GetComponent<BeatManager>().multiplier;
            animator.speed = 0.3f * GameObject.Find("BeatManager").GetComponent<BeatManager>().multiplier;
            animator.SetTrigger("GroundAttack" + attackIndex);
            body.AddForce(new Vector3(8 * body.gameObject.GetComponent<Move>().facing, 0, 0), ForceMode2D.Impulse);
            Debug.Log("Player Combo Fired");
            GameObject.Find("Slash2Sound").GetComponent<AudioSource>().Play();
        } 
        
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        if (shouldCombo)
        {
            stateMachine.SetNextState(new GroundFinisherState());
        }

        if (fixedtime >= duration)
        {
            animator.speed = 1;
            stateMachine.SetNextStateToMain();
        }
    }
}
