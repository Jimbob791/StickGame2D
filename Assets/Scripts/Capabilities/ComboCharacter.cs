using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboCharacter : MonoBehaviour
{

    private StateMachine meleeStateMachine;

    [SerializeField] public Collider2D hitbox;
    [SerializeField] public GameObject Hiteffect;

    // Start is called before the first frame update
    void Start()
    {
        meleeStateMachine = GetComponent<StateMachine>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0) && meleeStateMachine.CurrentState.GetType() == typeof(IdleCombatState))
        {
            if (transform.parent.GetComponent<Ground>().GetOnGround() == true)
            {
                if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
                    meleeStateMachine.SetNextState(new GroundEntryState());
            }
            else
            {
                meleeStateMachine.SetNextStateToMain();
            }
        }
    }
}
