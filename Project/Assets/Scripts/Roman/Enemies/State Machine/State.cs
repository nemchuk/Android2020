using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//all states are going to derives class
public class State 
{
    //list of vars all states will contain
    protected FiniteStateMachine stateMachine;
    protected Entity entity;

    protected string animBoolName;

    public float startTime { get; protected set; }

    //Constructor
    public State( Entity _entity , FiniteStateMachine _stateMachine , string _animBoolName)
    {
        entity = _entity;

        stateMachine = _stateMachine;

        animBoolName = _animBoolName;

    }

    //virtual means that this function can be redefined in derived classes
    public virtual void Enter()
    {
        startTime = Time.time; // to keep track of state start time

        entity.aliveAnim.SetBool(animBoolName, true);

        DoChecks();

    }

    public virtual void Exit()
    {
        entity.aliveAnim.SetBool(animBoolName, false);
    }

    public virtual void LogicUpdate()
    {

    }

    public virtual void PhysicsUpdate()
    {
        DoChecks();
    }

    public virtual void DoChecks()
    {

    }
          
}
