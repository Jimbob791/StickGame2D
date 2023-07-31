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
                EventManager.current.StartInputAction();
            }
            return;
        }
        
        if (groundMoves)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (Input.GetKey(KeyCode.W))
                {
                    meleeStateMachine.SetNextState(new AirBonusState());
                    EventManager.current.StartInputAction();
                }
                else
                {
                    meleeStateMachine.SetNextState(new GroundEntryState());
                    EventManager.current.StartInputAction();
                }
            }
            else if (Input.GetKeyDown(KeyCode.R))
            {
                meleeStateMachine.SetNextState(new GroundSnapState());
                EventManager.current.StartInputAction();
            }
            else if (Input.GetKeyDown(KeyCode.E))
            {
                meleeStateMachine.SetNextState(new BaseWarpState());
                EventManager.current.StartInputAction();
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
                EventManager.current.StartInputAction();
            }
            else if (Input.GetKeyDown(KeyCode.R))
            {
                if(Input.GetKey(KeyCode.S))
                {
                    meleeStateMachine.SetNextState(new AirSlamState());
                    EventManager.current.StartInputAction();
                }
                else
                {
                    meleeStateMachine.SetNextState(new AirStrikeState());
                    EventManager.current.StartInputAction();
                }
            }
            else if (Input.GetKeyDown(KeyCode.E))
            {
                meleeStateMachine.SetNextState(new BaseWarpState());
                EventManager.current.StartInputAction();
            }
            else
            {
                meleeStateMachine.SetNextStateToMain();
            }
        }       
    }
}
