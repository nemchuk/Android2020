using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer_MoveState : MoveState
{

    private Archer archer;

    public Archer_MoveState(Entity _entity, FiniteStateMachine _stateMachine, string _animBoolName, D_MoveState _stateData, Archer _archer) : base(_entity, _stateMachine, _animBoolName, _stateData)
    {
        archer = _archer;
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

        if (isPlayerInMinAgroRange)
        {
           stateMachine.ChangeState(archer.playerDetectedState);
        }

        else if (isDetectingWall || !isDetectingLedge)
        {
            archer.idleState.SetFlip(true);
            stateMachine.ChangeState(archer.idleState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
