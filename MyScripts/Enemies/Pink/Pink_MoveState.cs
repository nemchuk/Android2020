using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pink_MoveState : MoveState
{
    private Pink pink;//reference on a type of enemy

    public Pink_MoveState(Entity _entity, FiniteStateMachine _stateMachine, string _animBoolName, D_MoveState _stateData,Pink _pink) : base(_entity, _stateMachine, _animBoolName, _stateData)
    {
        pink = _pink;
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

        else if (isDetectingWall || !isDetectingLedge)
        {
            pink.idleState.SetFlip(true);
            stateMachine.ChangeState(pink.idleState);
        }

    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
