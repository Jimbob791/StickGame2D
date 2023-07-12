using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirSlamState : MagicBaseState
{
    private bool canMove;

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
            duration = 2f / GameObject.Find("BeatManager").GetComponent<BeatManager>().multiplier;
            animator.speed = 0.5f * GameObject.Find("BeatManager").GetComponent<BeatManager>().multiplier;
            animator.SetTrigger("AirMagic" + attackIndex);
            Debug.Log("Player Slam " + attackIndex);
            body.velocity = new Vector3(0, 0.2f, 0);
            canMove = true;
        }
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        if (fixedtime >= duration / 2 && canMove)
        {
            body.AddForce(new Vector3(0, -20f, 0), ForceMode2D.Impulse);
            canMove = false;
        }

        if (fixedtime >= duration)
        {
            animator.speed = 1;
            stateMachine.SetNextStateToMain();
            animator.gameObject.GetComponent<MagicManager>().SkystrikeAttack();
        }
    }
}
