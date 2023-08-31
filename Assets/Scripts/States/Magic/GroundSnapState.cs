using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundSnapState : MagicBaseState
{
    private bool canMove;
    private bool canCast = true;

    public override void OnEnter(StateMachine _stateMachine)
    {
        base.OnEnter(_stateMachine);

        if (body.gameObject.GetComponent<PlayerHealth>().CheckMana(50) == false)
        {
            stateMachine.SetNextStateToMain();
            duration = 0f;
            canCast = false;
            return;
        }

        EventManager.current.StartInputAction();

        //Attack
        if (body.gameObject.GetComponent<Ground>().GetOnGround() == false)
        {
            stateMachine.SetNextStateToMain();
            duration = 0f;
        }
        else if(canCast)
        {
            attackIndex = 1;
            duration = 2f / GameObject.Find("BeatManager").GetComponent<BeatManager>().multiplier;
            animator.speed = 0.4f * GameObject.Find("BeatManager").GetComponent<BeatManager>().multiplier;
            animator.SetTrigger("GroundMagic" + attackIndex);
            Debug.Log("Player Snapped " + attackIndex);
            animator.gameObject.GetComponent<MagicManager>().SkyShardAttack();
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
