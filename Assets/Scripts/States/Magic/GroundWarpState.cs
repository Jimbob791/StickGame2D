using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundWarpState : MagicBaseState
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
            duration = 0.3333333f / GameObject.Find("BeatManager").GetComponent<BeatManager>().multiplier;
            animator.speed = 1f * GameObject.Find("BeatManager").GetComponent<BeatManager>().multiplier;
            animator.SetTrigger("GroundMagic" + attackIndex);
            Debug.Log("Player Warped " + attackIndex);
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
