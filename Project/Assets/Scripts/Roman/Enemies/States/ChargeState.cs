using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Enemy Charges on the player only for a certain amount of time.When this time is up we go back either to PlayerDetected state or different state

public class ChargeState : State
{
    protected D_ChargeState stateData;

    protected bool isDetectingLedge;
    protected bool isDetectingWall;
    protected bool isPlayerInMinAgroRnage;
    protected bool isChargeOver;
    protected bool performCloseRangeAction;
    
    public ChargeState(Entity _entity, FiniteStateMachine _stateMachine, string _animBoolName , D_ChargeState _stateData) : base(_entity, _stateMachine, _animBoolName)
    {
        stateData = _stateData;
    }

    public override void DoChecks()
    {
        base.DoChecks();

        isPlayerInMinAgroRnage = entity.CheckPlayerInMinAgroRange();
        isDetectingLedge = entity.CheckLedge();
        isDetectingWall = entity.CheckWall();

        performCloseRangeAction = entity.CheckPlayerForCloseRangeAction();

    }

    public override void Enter()
    {
        base.Enter();

        isChargeOver = false;

        entity.SetVelocity(stateData.chargeSpeed);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if(Time.time >= startTime + stateData.chargeTime)
        {
            isChargeOver = true;
        }

    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

    }
}
