using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseWarpState : MagicBaseState
{
    public override void OnEnter(StateMachine _stateMachine)
    {
        base.OnEnter(_stateMachine);

        if (body.gameObject.GetComponent<PlayerHealth>().CheckMana(20) == false)
        {
            stateMachine.SetNextStateToMain();
            duration = 0f;
            return;
        }

        EventManager.current.StartInputAction();

        //Attack
        if (body.gameObject.GetComponent<Ground>().GetOnGround() == false)
        {
            attackIndex = 1;
            duration = 0.3333333f / GameObject.Find("BeatManager").GetComponent<BeatManager>().multiplier;
            animator.speed = 1f * GameObject.Find("BeatManager").GetComponent<BeatManager>().multiplier;
            animator.SetTrigger("Warp" + attackIndex);
            Debug.Log("Player Warped " + attackIndex);

            Vector3 mousePos = Input.mousePosition;
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePos);
            worldPosition.z = 0;

            Vector3 offset = worldPosition - body.gameObject.GetComponent<Transform>().position;
            offset.Normalize();
            offset *= 5f;
            body.gameObject.GetComponent<Transform>().position = body.gameObject.GetComponent<Transform>().position + offset;
            body.velocity = Vector3.zero;
        }
        else
        {
            attackIndex = 1;
            duration = 0.3333333f / GameObject.Find("BeatManager").GetComponent<BeatManager>().multiplier;
            animator.speed = 1f * GameObject.Find("BeatManager").GetComponent<BeatManager>().multiplier;
            animator.SetTrigger("Warp" + attackIndex);
            Debug.Log("Player Warped " + attackIndex);

            Vector3 mousePos = Input.mousePosition;
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePos);
            worldPosition.z = 0;

            Vector3 offset = worldPosition - body.gameObject.GetComponent<Transform>().position;
            offset.Normalize();
            offset *= 5f;
            body.gameObject.GetComponent<Transform>().position = body.gameObject.GetComponent<Transform>().position + offset;
            body.velocity = Vector3.zero;
        }

        // Play SFX
        foreach (Transform child in animator.gameObject.transform)
        {
            if (child.gameObject.name == "TeleportSound")
            {
                child.gameObject.GetComponent<AudioSource>().Play();
            }
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
