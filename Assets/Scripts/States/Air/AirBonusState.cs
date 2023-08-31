using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirBonusState : MeleeBaseState
{
    public override void OnEnter(StateMachine _stateMachine)
    {
        base.OnEnter(_stateMachine);

        //Attack
        if (body.gameObject.GetComponent<Ground>().GetOnGround() == true)
        {
            attackIndex = 1;
            stunTime = 1.5f;
            damage = 12;
            knockback = new Vector2(2, 10);
            duration = 1.1f / GameObject.Find("BeatManager").GetComponent<BeatManager>().multiplier;
            animator.speed = 0.3f * GameObject.Find("BeatManager").GetComponent<BeatManager>().multiplier;
            animator.SetTrigger("GroundAirAttack3");
            body.velocity = new Vector3(body.gameObject.GetComponent<Move>().facing * 10f, 18f, 0f);
            GameObject.Find("AirSlashSound").GetComponent<AudioSource>().Play();
        }
        else
        {
            attackIndex = 3;
            stunTime = 1.5f;
            damage = 8;
            knockback = new Vector2(2, 8);
            duration = 1.1f / GameObject.Find("BeatManager").GetComponent<BeatManager>().multiplier;
            animator.speed = 0.3f * GameObject.Find("BeatManager").GetComponent<BeatManager>().multiplier;
            animator.SetTrigger("GroundAirAttack3");
            body.velocity = new Vector3(body.gameObject.GetComponent<Move>().facing * 10f, 5f, 0f);   
            GameObject.Find("AirSlashSound").GetComponent<AudioSource>().Play(); 
        }
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        if (fixedtime >= duration)
        {
            animator.speed = 1;
            body.gravityScale = 1f;
            stateMachine.SetNextStateToMain();
        }
    }
}
