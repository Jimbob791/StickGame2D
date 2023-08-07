using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Jump : MonoBehaviour
{
    [Header("Jump Variables")]
    [SerializeField] private Animator animator = null;
    [SerializeField, Range(0f, 100f)] private float jumpHeight = 3f;
    [SerializeField, Range(0, 5)] private int maxAirJumps = 0;
    [SerializeField, Range(0f, 5f)] private float downwardMovementMultiplier = 3f;
    [SerializeField, Range(0f, 5f)] private float upwardMovementMultiplier = 1.7f;
    [SerializeField, Range(0f, 5f)] private float stallMovementMultiplier = 0.5f;

    [Header("Input System")]
    public PlayerControls playerControls;
    private InputAction jump;

    private Rigidbody2D body;
    private Ground ground;
    private Vector2 velocity;

    private int jumpPhase;
    private float defaultGravityScale;

    private bool desiredJump, onGround;

    void Awake()
    {
        playerControls = new PlayerControls();

        body = GetComponent<Rigidbody2D>();
        ground = GetComponent<Ground>();

        defaultGravityScale = 1f;
    }

    void OnEnable()
    {
        jump = playerControls.Player.Jump;
        jump.Enable();
    }

    void Update()
    {
        desiredJump |= jump.ReadValue<float>() > 0f ? true : false;
    }

    private void FixedUpdate()
    {
        animator.ResetTrigger("Jump");
        
        onGround = ground.GetOnGround();
        velocity = body.velocity;

        if (onGround)
        {
            jumpPhase = 0;
        }

        if (desiredJump)
        {
            desiredJump = false;
            JumpAction();
        }

        if (body.velocity.y > 0.3f)
        {
            body.gravityScale = upwardMovementMultiplier;
        }
        else if (body.velocity.y < -0.3f)
        {
            body.gravityScale = downwardMovementMultiplier;
        }
        else if (body.velocity.y == 0)
        {
            body.gravityScale = defaultGravityScale;
        }
        else if (body.velocity.y <= 0.3f && body.velocity.y >= -0.3f)
        {
            body.gravityScale = stallMovementMultiplier;
        }

        body.velocity = velocity;

        animator.SetFloat("xVelocity", body.velocity.x);
        animator.SetFloat("yVelocity", body.velocity.y);
    }

    private void JumpAction()
    {
        if (animator.GetFloat("Attacking") != 0f)
            return;

        if(onGround || jumpPhase < maxAirJumps)
        {
            jumpPhase += 1;
            animator.SetTrigger("Jump");
            float jumpSpeed = Mathf.Sqrt(-2f * Physics2D.gravity.y * jumpHeight);

            if(velocity.y > 0f)
            {
                jumpSpeed = Mathf.Max(jumpSpeed - velocity.y, 0f);
            }
            else if(velocity.y < 0f)
            {
                jumpSpeed += Mathf.Abs(body.velocity.y);
            }

            velocity.y += jumpSpeed;
        }
    }
}
