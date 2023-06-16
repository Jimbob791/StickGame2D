using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeBaseState : State
{
    public float duration;
    protected Animator animator;
    protected bool shouldCombo = false;
    protected int attackIndex;

    protected Collider2D hitCollider;
    private List<Collider2D> collidersDamaged;
    private GameObject hitEffectPrefab;

    private float attackPressedTimer = 0;

    public override void OnEnter(StateMachine _stateMachine)
    {
        base.OnEnter(_stateMachine);

        foreach (Transform child in GetComponent<Transform>())
        {
            if(child.name == "Sprite")
            animator = child.gameObject.GetComponent<Animator>();
        }
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        if (Input.GetMouseButtonDown(0))
        {
            shouldCombo = true;
        }
    }

    public override void OnExit()
    {
        base.OnExit();
    }

}
