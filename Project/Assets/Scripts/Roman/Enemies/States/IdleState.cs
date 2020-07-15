using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//What Do I want from IDLE state is that an enemy when gets to wall or ledge stay in IDLE for a little amount of time an than switches to another state

public class IdleState : State
{
    protected D_IdleState stateData;

    protected bool flipAfterIdle;
    protected bool isIdleTimeOver;
    protected bool isPlayerInMinAgroRange;

    protected float idleTime;

    public IdleState(Entity _entity, FiniteStateMachine _stateMachine, string _animBoolName , D_IdleState _stateData) : base(_entity, _stateMachine, _animBoolName)
    {
        stateData = _stateData;
    }

    public override void DoChecks()
    {
        base.DoChecks();

        isPlayerInMinAgroRange = entity.CheckPlayerInMinAgroRange();

    }

    //When we enter the Idle state we want to stop moving enemy
    public override void Enter()
    {
        base.Enter();

        entity.SetVelocity(0f);

        isIdleTimeOver = false;

        SetRandomIdleTime();

    }

    //When we leave IDLE state we want to flip an enemy
    public override void Exit()
    {
        base.Exit();

        if(flipAfterIdle)
        {
            entity.Flip();
        }

    }

    //We check if we are enough in IDLE state and are ready to swith to another state
    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if(Time.time >= startTime + idleTime)
        {
            isIdleTimeOver = true;
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

    }

    public void SetFlip(bool _flip)
    {
        flipAfterIdle = _flip;
    }

    private void SetRandomIdleTime()
    {
        idleTime = Random.Range( stateData.minIdleTime , stateData.maxIdleTime);
    }

}
