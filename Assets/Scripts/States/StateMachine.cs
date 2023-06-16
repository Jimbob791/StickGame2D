using UnityEngine;

public class StateMachine : MonoBehaviour
{
    public string customName;

    private State mainStateType;

    public State currentState { get; private set; }
    private State nextState;

    void Update()
    {
        if (nextState != null)
        {
            SetState(nextState);
        }

        if (currentState != null)
            currentState.OnUpdate();
    }

    private void SetState(State _newState)
    {
        nextState = null;
        if (currentState != null)
        {
            currentState.OnExit();
        }
        currentState = _newState;
        currentState.OnEnter(this);
    }

    public void SetNextState(State _newState)
    {
        if (_newState != null)
        {
            nextState = _newState;
        }
    }

    private void LateUpdate()
    {
        if (currentState != null)
            currentState.OnLateUpdate();
    }

    private void FixedUpdate()
    {
        if (currentState != null)
            currentState.OnFixedUpdate();
    }

    public void SetNextStateToMain()
    {
        nextState = mainStateType;
    }

    private void Awake()
    {
        SetNextStateToMain();
    }


    private void OnValidate()
    {
        if (mainStateType == null)
        {
            if (customName == "Combat")
            {
                mainStateType = new IdleCombatState();
            }
        }
    }
}