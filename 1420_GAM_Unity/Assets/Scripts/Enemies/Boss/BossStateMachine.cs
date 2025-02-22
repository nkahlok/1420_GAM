using UnityEngine;

public class BossStateMachine : MonoBehaviour
{
    public BossState currentState;

    public void Initialize(BossState _startState)
    {
        currentState = _startState;
        currentState.Enter();
    }

    public void ChangeState(BossState newState)
    {
        currentState.Exit();
        currentState = newState;
        currentState.Enter();
    }

}
