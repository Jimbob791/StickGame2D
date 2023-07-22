using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboCharacter : MonoBehaviour
{
    private StateMachine meleeStateMachine;

    [SerializeField] public Collider2D hitbox;
    [SerializeField] public GameObject Hiteffect;
    private bool groundMoves;

    void Start()
    {
        meleeStateMachine = GetComponent<StateMachine>();
    }

    // Update is called once per frame
    void Update()
    {
        groundMoves = transform.parent.GetComponent<Ground>().GetOnGround();

        if (meleeStateMachine.CurrentState.GetType() != typeof(IdleCombatState))
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                meleeStateMachine.SetNextState(new BaseWarpState());
            }
            return;
        }
        
        if (groundMoves)
        {
            if (Input.GetMouseButtonDown(0))
            {
                meleeStateMachine.SetNextState(new GroundEntryState());
            }
            else if (Input.GetKeyDown(KeyCode.R))
            {
                meleeStateMachine.SetNextState(new GroundSnapState());
            }
            else if (Input.GetKeyDown(KeyCode.E))
            {
                meleeStateMachine.SetNextState(new BaseWarpState());
            }
            else
            {
                meleeStateMachine.SetNextStateToMain();
            }
        }
        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                meleeStateMachine.SetNextState(new AirEntryState());
            }
            else if (Input.GetKeyDown(KeyCode.R))
            {
                if(Input.GetKey(KeyCode.S))
                {
                    meleeStateMachine.SetNextState(new AirSlamState());
                }
                else
                {
                    meleeStateMachine.SetNextState(new AirStrikeState());
                }
            }
            else if (Input.GetKeyDown(KeyCode.E))
            {
                meleeStateMachine.SetNextState(new BaseWarpState());
            }
            else
            {
                meleeStateMachine.SetNextStateToMain();
            }
        }       
    }
}
