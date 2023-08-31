using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirStrikeState : MagicBaseState
{
    private bool canMove;
    private bool canCast = true;
    
    public override void OnEnter(StateMachine _stateMachine)
    {
        base.OnEnter(_stateMachine);

        if (body.gameObject.GetComponent<PlayerHealth>().CheckMana(75) == false)
        {
            stateMachine.SetNextStateToMain();
            duration = 0f;
            return;
        }

        EventManager.current.StartInputAction();

        //Attack
        if (body.gameObject.GetComponent<Ground>().GetOnGround() == true)
        {
            stateMachine.SetNextStateToMain();
            duration = 0f;
        }
        else
        {
            attackIndex = 1;
            duration = 0.666666666f / GameObject.Find("BeatManager").GetComponent<BeatManager>().multiplier;
            animator.speed = 0.66666666f * GameObject.Find("BeatManager").GetComponent<BeatManager>().multiplier;
            animator.SetTrigger("AirMagic" + attackIndex);
            Debug.Log("Player Struck " + attackIndex);
            animator.gameObject.GetComponent<MagicManager>().SkystrikeAttack();
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
