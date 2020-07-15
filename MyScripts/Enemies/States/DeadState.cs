using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadState : State
{

    protected D_DeadState stateData;


    public DeadState(Entity _entity, FiniteStateMachine _stateMachine, string _animBoolName , D_DeadState _stateData) : base(_entity, _stateMachine, _animBoolName)
    {
        stateData = _stateData;
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();


        entity.SetVelocity(0f);
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
