using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    [SerializeField] private Animator animator = null;
    [SerializeField] private Transform sprite = null;
    [SerializeField] private GameObject player = null;
    [SerializeField, Range(0f, 100f)] private float maxSpeed = 4f;
    [SerializeField, Range(0f, 100f)] private float maxAcceleration = 35f;

    private Vector2 direction;
    private Vector2 desiredVelocity;
    private Vector2 velocity;
    private Rigidbody2D body;
    private Ground ground;
    
    private int spriteFlip;
    private float maxSpeedChange;
    private float acceleration;
    private float state = "Idle";
    private bool onGround;

    void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        ground = GetComponent<Ground>();
    }

    void Update()
    {
        onGround = ground.GetOnGround();
        animator.SetBool("Grounded", onGround);

        RaycastHit2D leftCast = Physics2D.CircleCast(transform.position, 2f, new Vector2(-1, 0), 6f, playerLayer);
        RaycastHit2D rightCast = Physics2D.CircleCast(transform.position, 2f, new Vector2(1, 0), 6f, playerLayer);

        if (leftCast != null || rightCast != null)
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
                if (Vecto2.Distance(transform.position, player.transform.position) <= 2f)
                {
                    animator.SetTrigger("Attack1");
                }
            }
            else if (state == "Attack")
            {
                if (Vecto2.Distance(transform.position, player.transform.position) <= 2f)
                {
                    animator.SetTrigger("Attack1");
                }
                else if (leftCast != null)
                {
                    direction.x = -1f;
                }
                else if (rightCast != null)
                {
                    direction.x = 1f;
                }
            }
        }
        else
        {
            direction.x = 0f;
        }

        desiredVelocity = new Vector2(direction.x, 0f) * Mathf.Max(maxSpeed - ground.GetFriction(), 0f);

        if (direction.x != 0)
        {
            sprite.localScale = new Vector3(direction.x, 1f, 1f);
        }

        animator.SetFloat("Running", Mathf.Abs(direction.x));
    }

    private void FixedUpdate()
    {
        velocity = body.velocity;

        acceleration = onGround ? maxAcceleration : maxAirAcceleration;
        maxSpeedChange = acceleration * Time.deltaTime;
        velocity.x = Mathf.MoveTowards(velocity.x, desiredVelocity.x, maxSpeedChange);

        body.velocity = velocity;
    }
}
