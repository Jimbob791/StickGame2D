using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicBaseState : State
{
    // How long this state should be active for before moving on
    public float duration;
    // Cached animator component
    protected Animator animator;
    // Cached player rigidbody component
    protected Rigidbody2D body;
    // bool to check whether or not the next attack in the sequence should be played or not
    protected bool shouldCombo = false;
    // The attack index in the sequence of attacks
    protected int attackIndex;
    // The knockback vector to apply
    protected Vector2 knockback;
    // The knockback time to apply
    protected float stunTime;

    public override void OnEnter(StateMachine _stateMachine)
    {
        base.OnEnter(_stateMachine);
        animator = GetComponent<Animator>();
        body = GetComponent<Transform>().parent.GetComponent<Rigidbody2D>();
    }

   public override void OnUpdate()
    {
        base.OnUpdate();
    }

    public override void OnExit()
    {
        base.OnExit();
    }
}
