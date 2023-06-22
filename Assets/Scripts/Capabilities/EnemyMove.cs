using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    [SerializeField] private Animator animator = null;
    [SerializeField] private Transform sprite = null;
    [SerializeField] private GameObject player = null;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField, Range(0f, 100f)] private float maxSpeed = 4f;
    [SerializeField, Range(0f, 100f)] private float maxAcceleration = 35f;

    private Vector2 goal;
    private Vector2 direction;    
    private Vector2 desiredVelocity;
    private Vector2 velocity;
    private Rigidbody2D body;
    private Ground ground;
    
    private int spriteFlip;
    private float hitStun;
    private float maxSpeedChange;
    private float acceleration;
    private string state = "Idle";
    private bool onGround;

    void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        ground = GetComponent<Ground>();
        StartCoroutine(SetGoal());
    }

    void Update()
    {
        hitStun -= Time.deltaTime;

        onGround = ground.GetOnGround();
        animator.SetBool("Grounded", onGround);

        if (body.velocity.x != 0)
        {
            if (body.velocity.x > 0)
            {
                sprite.localScale = new Vector3(1f, 1f, 1f);
            }
            if (body.velocity.x < 0)
            {
                sprite.localScale = new Vector3(-1f, 1f, 1f);
            }
        }

        if (state == "Stunned")
        {
            if (onGround && hitStun <= 0)
            {
                state = "Idle";
                animator.SetBool("Stunned", false);
            }
            return;
        }

        RaycastHit2D leftCast = Physics2D.CircleCast(transform.position, 1f, new Vector2(-1, 0), 4f, playerLayer);
        RaycastHit2D rightCast = Physics2D.CircleCast(transform.position, 1f, new Vector2(1, 0), 4f, playerLayer);

        if (leftCast || rightCast)
        {
            state = "Attack";
        }
        else
        {
            state = "Idle";
        }

        if (animator.GetFloat("Attacking") == 0f)
        {
            if (state == "Idle")
            {
                direction.x = goal.x;
            }
            else if (state == "Attack")
            {
                if (Vector2.Distance(transform.position, player.transform.position) <= 1.5f)
                {
                    animator.SetTrigger("Attack1");
                }

                if (Vector2.Distance(transform.position, player.transform.position) > 1.5f)
                {
                    animator.ResetTrigger("Attack1");
                    if (leftCast)
                    {
                        direction.x = -1f;
                    }
                    else if (rightCast)
                    {
                        direction.x = 1f;
                    }
                }
                
            }
        }
        else
        {
            direction.x = 0f;
        }

        desiredVelocity = new Vector2(direction.x, 0f) * Mathf.Max(maxSpeed - ground.GetFriction(), 0f);

        animator.SetFloat("Running", Mathf.Abs(direction.x));
    }

    void FixedUpdate()
    {
        if (state == "Stunned")
        {
            return;
        }

        velocity = body.velocity;

        acceleration = maxAcceleration;
        maxSpeedChange = acceleration * Time.deltaTime;
        velocity.x = Mathf.MoveTowards(velocity.x, desiredVelocity.x, maxSpeedChange);

        body.velocity = velocity;
    }

    IEnumerator SetGoal()
    {
        int r = Random.Range(0, 3);
        if (r == 0)
        {
            goal.x = 1;
        }
        else if (r == 1)
        {
            goal.x = -1;
        }
        else if (r >= 2)
        {
            goal.x = 0;
        }

        yield return new WaitForSeconds(Random.Range(1f, 3.5f));
        StartCoroutine(SetGoal());
    }

    public void Hit(int damage, Vector2 knockback, float stunTime)
    {
        animator.SetBool("Stunned", true);
        state = "Stunned";
        animator.ResetTrigger("Attack1");
        hitStun = stunTime;
        if (player.transform.position.x >= transform.position.x)
        {
            knockback.x *= -1;
        }
        body.AddForce(knockback, ForceMode2D.Impulse);
        gameObject.GetComponent<EnemyHealth>().ChangeHealth(damage);
    }
}
