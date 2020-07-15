using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pink_LookForPlayerState : LookForPlayerState
{

    private Pink pink;

    public Pink_LookForPlayerState(Entity _entity, FiniteStateMachine _stateMachine, string _animBoolName, D_LookForPlayerState _stateData, Pink _pink) : base(_entity, _stateMachine, _animBoolName, _stateData)
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

        if(isPlayerInMinAgroRange)
        {
            stateMachine.ChangeState(pink.playerDetectedState);
        }
        else if(isAllTurnsTimeLeft)
        {
            stateMachine.ChangeState(pink.moveState);
        }

    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
