using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine 
{

    public Player player;
    public PlayerState currentState;

    public void Initialize(PlayerState _startState)
    {
        currentState = _startState;
        currentState.Enter();

    }

    public void Changestate(PlayerState _newState)
    {
        currentState.Exit();
        currentState = _newState;
        currentState.Enter();

    }

}
