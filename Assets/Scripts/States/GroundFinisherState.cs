using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundFinisherState : MeleeBaseState
{
    private bool moved = false;

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
            duration = 0.5f;
            animator.SetTrigger("Attack" + attackIndex);
            Debug.Log("Player Attack " + attackIndex + " Fired!");
        }
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        if (fixedtime >= 0.12f && moved == false)
        {
            moved = true;
            body.AddForce(new Vector3(body.gameObject.GetComponent<Transform>().localScale.x, 0f, 0f) * 8f, ForceMode2D.Impulse);
        }

        if (fixedtime >= duration + 0.25f)
        {
             stateMachine.SetNextStateToMain();
        }
    }
}
