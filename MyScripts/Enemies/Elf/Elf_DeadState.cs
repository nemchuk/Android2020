using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elf_DeadState : DeadState
{
    private Elf elf;
    public Elf_DeadState(Entity _entity, FiniteStateMachine _stateMachine, string _animBoolName, D_DeadState _stateData, Elf _elf) : base(_entity, _stateMachine, _animBoolName, _stateData)
    {
        elf = _elf;
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
