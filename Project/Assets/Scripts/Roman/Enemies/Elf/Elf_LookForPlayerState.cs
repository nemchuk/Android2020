using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elf_LookForPlayerState : LookForPlayerState
{

    private Elf elf;

    public Elf_LookForPlayerState(Entity _entity, FiniteStateMachine _stateMachine, string _animBoolName, D_LookForPlayerState _stateData,Elf _elf) : base(_entity, _stateMachine, _animBoolName, _stateData)
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

        if (isPlayerInMinAgroRange)
        {
            stateMachine.ChangeState(elf.playerDetectedState);
        }
        else if (isAllTurnsTimeLeft)
        {
            stateMachine.ChangeState(elf.moveState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
