using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ComboCharacter : MonoBehaviour
{
    private StateMachine meleeStateMachine;

    [SerializeField] public Collider2D hitbox;
    [SerializeField] public GameObject Hiteffect;
    private bool groundMoves;

    [Header("Input System")]
    private PlayerControls playerControls;
    private InputAction basicAttack;
    private InputAction magicAttack;
    private InputAction teleport;
    private InputAction lookUp;
    private InputAction lookDown;

    void Awake()
    {
        playerControls = new PlayerControls();
    }

    void OnEnable()
    {
        basicAttack = playerControls.Player.BasicAttack;
        basicAttack.Enable();
        magicAttack = playerControls.Player.BasicMagicAttack;
        magicAttack.Enable();
        teleport = playerControls.Player.Teleport;
        teleport.Enable();
        lookUp = playerControls.Player.LookUp;
        lookUp.Enable();
        lookDown = playerControls.Player.LookDown;
        lookDown.Enable();
    }

    void Start()
    {
        meleeStateMachine = GetComponent<StateMachine>();
    }

    // Update is called once per frame
    void Update()
    {
        groundMoves = gameObject.GetComponent<Ground>().GetOnGround();

        if (meleeStateMachine.CurrentState.GetType() != typeof(IdleCombatState))
        {
            if (teleport.triggered && teleport.ReadValue<float>() > 0f)
            {
                meleeStateMachine.SetNextState(new BaseWarpState());
            }
            return;
        }
        
        if (groundMoves)
        {
            if (basicAttack.triggered && basicAttack.ReadValue<float>() > 0f)
            {
                if (lookUp.ReadValue<float>() > 0f)
                {
                    meleeStateMachine.SetNextState(new AirBonusState());
                    EventManager.current.StartInputAction();
                }
                else
                {
                    meleeStateMachine.SetNextState(new GroundEntryState());
                    EventManager.current.StartInputAction();
                }
            }
            else if (magicAttack.triggered && magicAttack.ReadValue<float>() > 0f)
            {
                meleeStateMachine.SetNextState(new GroundSnapState());
                EventManager.current.StartInputAction();
            }
            else if (teleport.triggered && teleport.ReadValue<float>() > 0f)
            {
                meleeStateMachine.SetNextState(new BaseWarpState());
            }
            else
            {
                meleeStateMachine.SetNextStateToMain();
            }
        }
        else
        {
            if (basicAttack.triggered && basicAttack.ReadValue<float>() > 0f)
            {
                meleeStateMachine.SetNextState(new AirEntryState());
                EventManager.current.StartInputAction();
            }
            else if (magicAttack.triggered && magicAttack.ReadValue<float>() > 0f)
            {
                if(lookDown.ReadValue<float>() > 0f)
                {
                    meleeStateMachine.SetNextState(new AirSlamState());
                    EventManager.current.StartInputAction();
                }
                else
                {
                    meleeStateMachine.SetNextState(new AirStrikeState());
                    EventManager.current.StartInputAction();
                }
            }
            else if (teleport.triggered && teleport.ReadValue<float>() > 0f)
            {
                meleeStateMachine.SetNextState(new BaseWarpState());
            }
            else
            {
                meleeStateMachine.SetNextStateToMain();
            }
        }       
    }
}
