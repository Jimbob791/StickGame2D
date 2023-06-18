using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    [SerializeField] private InputController input = null;
    [SerializeField] private Animator animator = null;
    [SerializeField] private Transform sprite = null;
    [SerializeField, Range(0f, 100f)] private float maxSpeed = 4f;
    [SerializeField, Range(0f, 100f)] private float maxAcceleration = 35f;
    [SerializeField, Range(0f, 100f)] private float maxAirAcceleration = 20f;

    private Vector2 direction;
    private Vector2 desiredVelocity;
    private Vector2 velocity;
    private Rigidbody2D body;
    private Ground ground;
    
    private int spriteFlip;
    private float maxSpeedChange;
    private float acceleration;
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

        if (animator.GetFloat("Attacking") == 0f)
        {
            direction.x = input.RetrieveMoveInput();
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

        if (direction.x > 0 || direction.x < 0)
        {
            animator.SetBool("Running", true);
        }
        else
        {
            animator.SetBool("Running", false);
        }
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
