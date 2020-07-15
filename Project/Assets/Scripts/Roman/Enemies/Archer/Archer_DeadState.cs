using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer_DeadState : DeadState
{

    private Archer archer;

    public Archer_DeadState(Entity _entity, FiniteStateMachine _stateMachine, string _animBoolName, D_DeadState _stateData,Archer _archer) : base(_entity, _stateMachine, _animBoolName, _stateData)
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


    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
