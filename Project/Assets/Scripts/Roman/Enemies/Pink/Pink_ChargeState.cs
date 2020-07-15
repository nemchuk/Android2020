using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pink_ChargeState : ChargeState
{
    private Pink pink;

    public Pink_ChargeState(Entity _entity, FiniteStateMachine _stateMachine, string _animBoolName, D_ChargeState _stateData,Pink _pink) : base(_entity, _stateMachine, _animBoolName, _stateData)
    {
        pink = _pink;
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (performCloseRangeAction)
        {
            stateMachine.ChangeState(pink.meleeAttackState);
        }else
        if (!isDetectingLedge || isDetectingWall)
        {
            stateMachine.ChangeState(pink.lookForPlayerState);
        }
        else
        if(isChargeOver)
        {   
            if(isPlayerInMinAgroRnage)
            {
                stateMachine.ChangeState(pink.playerDetectedState);
            }
            else
            {
                stateMachine.ChangeState(pink.lookForPlayerState);
            }
        }

    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
