using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboCharacter : MonoBehaviour
{

    private StateMachine meleeStateMachine;

    [SerializeField] public Collider2D hitbox;
    [SerializeField] public GameObject hitEffect;

    void Start()
    {
        meleeStateMachine = GetComponent<StateMachine>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && meleeStateMachine.currentState.GetType() == typeof(IdleCombatState))
        {
            meleeStateMachine.SetNextState(new GroundEntryState());
        }
    }
}