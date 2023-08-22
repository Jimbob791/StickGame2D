using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirSlamState : MagicBaseState
{
    private bool canMove;
    private bool canCast = true;

    public override void OnEnter(StateMachine _stateMachine)
    {
        base.OnEnter(_stateMachine);

        if (body.gameObject.GetComponent<PlayerHealth>().CheckMana(60) == false)
        {
            stateMachine.SetNextStateToMain();
            duration = 0f;
            canCast = false;
            return;
        }

        //Attack
        if (body.gameObject.GetComponent<Ground>().GetOnGround() == true)
        {
            stateMachine.SetNextStateToMain();
            duration = 0f;
        }
        else
        {
            attackIndex = 2;
            duration = 2f / GameObject.Find("BeatManager").GetComponent<BeatManager>().multiplier;
            animator.speed = 0.5f * GameObject.Find("BeatManager").GetComponent<BeatManager>().multiplier;
            animator.SetTrigger("AirMagic" + attackIndex);
            Debug.Log("Player Slam " + attackIndex);
            body.AddForce(new Vector3(0, 8f, 0), ForceMode2D.Impulse);
            canMove = true;
        }
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        if (fixedtime >= duration / 2 && canMove)
        {
            body.AddForce(new Vector3(0, -200f, 0), ForceMode2D.Impulse);
            canMove = false;
        }

        if (fixedtime >= duration)
        {
            animator.speed = 1;
            stateMachine.SetNextStateToMain();
            if(canCast)
            {
                animator.gameObject.GetComponent<MagicManager>().SkySlamAttack();
            }
        }
    }
}
