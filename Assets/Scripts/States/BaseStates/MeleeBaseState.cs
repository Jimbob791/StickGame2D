using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeBaseState : State
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



    // The cached hit collider component of this attack
    protected Collider2D hitCollider;
    // Cached already struck objects of said attack to avoid overlapping attacks on same target
    private List<Collider2D> collidersDamaged;
    // The Hit Effect to Spawn on the hit Enemy
    private GameObject HitEffectPrefab;
    // damage to deal
    protected int damage;

    // Input buffer Timer
    private float AttackPressedTimer = 0;

    public override void OnEnter(StateMachine _stateMachine)
    {
        base.OnEnter(_stateMachine);
        animator = GetComponent<Animator>();
        body = GetComponent<Transform>().parent.GetComponent<Rigidbody2D>();
        collidersDamaged = new List<Collider2D>();
        hitCollider = GetComponent<ComboCharacter>().hitbox;
        HitEffectPrefab = GetComponent<ComboCharacter>().Hiteffect;
    }

   public override void OnUpdate()
    {
        base.OnUpdate();
        AttackPressedTimer -= Time.deltaTime;

        if (animator.GetFloat("WeaponActive") > 0f)
        {
            Attack();
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (animator.GetFloat("AttackWindow") > 0f)
            {
                shouldCombo = true;
            }
        }
    }

    public override void OnExit()
    {
        base.OnExit();
    }

    protected void Attack()
    {
        Collider2D[] collidersToDamage = new Collider2D[10];
        ContactFilter2D filter = new ContactFilter2D();
        filter.useTriggers = true;
        int colliderCount = Physics2D.OverlapCollider(hitCollider, filter, collidersToDamage);
        for (int i = 0; i < colliderCount; i++)
        {

            if (!collidersDamaged.Contains(collidersToDamage[i]))
            {
                TeamComponent hitTeamComponent = collidersToDamage[i].GetComponentInChildren<TeamComponent>();

                // Only check colliders with a valid Team Componnent attached
                if (hitTeamComponent && hitTeamComponent.teamIndex == TeamIndex.Enemy)
                {
                    GameObject.Instantiate(HitEffectPrefab, collidersToDamage[i].gameObject.transform.position, Quaternion.identity);
                    collidersDamaged.Add(collidersToDamage[i]);
                    collidersToDamage[i].gameObject.GetComponent<EnemyMove>().Hit(-1 * damage, knockback, stunTime, body.gameObject, "sword");
                }
            }
        }
    }

}
