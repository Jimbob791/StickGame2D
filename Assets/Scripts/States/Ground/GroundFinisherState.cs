using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundFinisherState : MeleeBaseState
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
            attackIndex = 3;
            stunTime = 1.5f;
            damage = 8;
            knockback = new Vector2(4, 6);
            duration = 1.9f / GameObject.Find("BeatManager").GetComponent<BeatManager>().multiplier;
            animator.speed = 0.3f * GameObject.Find("BeatManager").GetComponent<BeatManager>().multiplier;
            animator.SetTrigger("GroundAttack" + attackIndex);
            //Debug.Log("Player Attack " + attackIndex + " Fired!");
            GameObject.Find("Slash3Sound").GetComponent<AudioSource>().Play();
        }
        
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        if (fixedtime >= duration)
        {
            animator.speed = 1;
            stateMachine.SetNextStateToMain();
        }
    }
}
