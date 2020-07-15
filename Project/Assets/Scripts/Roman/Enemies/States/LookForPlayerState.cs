using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//If the enemy is not detecting the player,we want just to keep looking in a certain direction for a little amount of time
//Than to turn around, look in that direction a bit, turn back again and transition to another state if we re not detecting a player
//If we do detect the player during this time< we go back to PlayerDetectedState
public class LookForPlayerState : State
{
    protected D_LookForPlayerState stateData;

    protected bool isPlayerInMinAgroRange;
    protected bool isAllTurnsDone;
    protected bool isAllTurnsTimeLeft;

    protected float lastTurnTime;
    protected int numberOfTurnsDone;

    protected bool turnImmediately;

    public LookForPlayerState(Entity _entity, FiniteStateMachine _stateMachine, string _animBoolName,D_LookForPlayerState _stateData) : base(_entity, _stateMachine, _animBoolName)
    {
        stateData = _stateData;
    }

    public override void DoChecks()
    {
        base.DoChecks();

        isPlayerInMinAgroRange = entity.CheckPlayerInMinAgroRange();

    }

    public override void Enter()
    {
        base.Enter();

        isAllTurnsDone = false;
        isAllTurnsTimeLeft = false;

        lastTurnTime = startTime;
        numberOfTurnsDone = 0;

        entity.SetVelocity(0f);

    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if(turnImmediately)
        {
            entity.Flip();
            lastTurnTime = Time.time;
            numberOfTurnsDone++;
            turnImmediately = false;
        }
        else if(Time.time >= lastTurnTime + stateData.timeBetweenTurns && !isAllTurnsDone)
        {
            entity.Flip();
            lastTurnTime = Time.time;
            numberOfTurnsDone++;
        }

        if(numberOfTurnsDone >= stateData.turnsNumber)
        {
            isAllTurnsDone = true;
        }

        if(Time.time >= lastTurnTime + stateData.timeBetweenTurns && isAllTurnsDone)
        {
            isAllTurnsTimeLeft = true;
        }

    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public void SetTurnImmediately(bool _flip)
    {
        turnImmediately = _flip;
    }
}
