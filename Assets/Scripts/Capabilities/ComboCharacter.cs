using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboCharacter : MonoBehaviour
{
    private StateMachine meleeStateMachine;

    [SerializeField] public Collider2D hitbox;
    [SerializeField] public GameObject Hiteffect;

    void Start()
    {
        meleeStateMachine = GetComponent<StateMachine>();
    }

    // Update is called once per frame
    void Update()
    {
        if (meleeStateMachine.CurrentState.GetType() == typeof(IdleCombatState))
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (transform.parent.GetComponent<Ground>().GetOnGround() == true)
                {
                    meleeStateMachine.SetNextState(new GroundEntryState());
                }
                else
                {
                    meleeStateMachine.SetNextStateToMain();
                }
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                if (transform.parent.GetComponent<Ground>().GetOnGround() == true)
                {
                    meleeStateMachine.SetNextState(new GroundSnapState());
                }
                else
                {
                    meleeStateMachine.SetNextState(new AirStrikeState());
                }
            }

            if (Input.GetKeyDown(KeyCode.S) && Input.GetKeyDown(KeyCode.R))
            {
                if (transform.parent.GetComponent<Ground>().GetOnGround() == true)
                {
                    meleeStateMachine.SetNextStateToMain();
                }
                else
                {
                    meleeStateMachine.SetNextState(new AirSlamState());
                }
            }
        }        
    }
}
