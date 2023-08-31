using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Move : MonoBehaviour
{
    [Header("Cape Offsets (Assume facing right)")]
    [SerializeField] private Vector2 idleOffset = new Vector2(-0.01f, -0.1f);
    [SerializeField] private float capeXMulti = 100;
    [SerializeField] private float capeYMulti = 100;

    [Header("Cape Anchor")]
    [SerializeField] private CapeAnchor capeAnchor;

    [Header("Input System")]
    private PlayerControls playerControls;
    private InputAction move;

    [Header("Input Variables")]
    [SerializeField] private Animator animator = null;
    [SerializeField] private Transform sprite = null;
    [SerializeField, Range(0f, 100f)] private float maxSpeed = 4f;
    [SerializeField, Range(0f, 100f)] private float maxAcceleration = 35f;
    [SerializeField, Range(0f, 100f)] private float maxAirAcceleration = 20f;

    public float facing;
    private Vector2 direction;
    private Vector2 desiredVelocity;
    private Vector2 velocity;
    private Rigidbody2D body;
    private Ground ground;
    private GameObject lastCheckpoint;
    
    private int spriteFlip;
    private float maxSpeedChange;
    private float acceleration;
    private bool onGround;

    void OnEnable()
    {
        move = playerControls.Player.Move;
        move.Enable();
    }

    void OnDisable()
    {
        move.Disable();
    }

    void Awake()
    {
        playerControls = new PlayerControls();
        body = GetComponent<Rigidbody2D>();
        ground = GetComponent<Ground>();
    }

    void Update()
    {
        onGround = ground.GetOnGround();

        if (animator.GetFloat("Attacking") == 0f)
        {
            direction.x = move.ReadValue<Vector2>().x;
            if (direction.x != 0f)
            {
                direction.Normalize();
                facing = direction.x;
            }
        }
        else
        {
            direction.x = 0f;
        }

        desiredVelocity = new Vector2(direction.x, 0f) * Mathf.Max(maxSpeed - ground.GetFriction(), 0f);

        if (direction.x != 0)
        {
            sprite.localScale = new Vector3(facing, 1f, 1f);
        }

        UpdateCapeOffset();

        animator.SetFloat("Running", Mathf.Abs(direction.x));
        animator.SetBool("Grounded", onGround);
    }

    private void FixedUpdate()
    {
        velocity = body.velocity;

        acceleration = onGround ? maxAcceleration : maxAirAcceleration;
        maxSpeedChange = acceleration * Time.deltaTime;
        velocity.x = Mathf.MoveTowards(velocity.x, desiredVelocity.x, maxSpeedChange);

        body.velocity = velocity;
    }

    private void UpdateCapeOffset()
    {
        Vector2 currentOffset = idleOffset;

        if (body.velocity.y != 0)
        {
            currentOffset = Vector2.zero;
        }

        currentOffset.x += (-1f * body.velocity.x) / capeXMulti;
        currentOffset.y += (-1f * body.velocity.y) / capeYMulti;

        capeAnchor.partOffset = currentOffset;        
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log(col.gameObject.tag);
        if (col.gameObject.tag == "Checkpoint")
        {
            lastCheckpoint = col.gameObject;
        }
        if (col.gameObject.tag == "Void")
        {
            transform.position = lastCheckpoint.transform.position;
            body.velocity = Vector3.zero;
        }
        if (col.gameObject.tag == "Portal")
        {
            GameObject.Find("LoadManager").GetComponent<LoadManager>().isWin = true;
            GameObject.Find("LoadManager").GetComponent<LoadManager>().LevelComplete();
        }
    }
}
