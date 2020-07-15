using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiniteStateMachine
{
    public State currentState { get; private set; } // it means that this var is accesable from any class to get,but can be set only within this class

    //Initialize state in enemy
    public void Initialize(State _startState)
    {
        currentState = _startState;

        currentState.Enter();
    }

    public void ChangeState(State _newState)
    {
        currentState.Exit();

        currentState = _newState;

        currentState.Enter();
    }

}
