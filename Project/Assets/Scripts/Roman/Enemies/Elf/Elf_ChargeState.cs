using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elf_ChargeState : ChargeState
{

    private Elf elf;

    public Elf_ChargeState(Entity _entity, FiniteStateMachine _stateMachine, string _animBoolName, D_ChargeState _stateData, Elf _elf) : base(_entity, _stateMachine, _animBoolName, _stateData)
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

        if (performCloseRangeAction)
        {
            stateMachine.ChangeState(elf.meleeAttackState);
        }
        else
        if (!isDetectingLedge || isDetectingWall)
        {
            stateMachine.ChangeState(elf.lookForPlayerState);
        }
        else
        if (isChargeOver)
        {
            if (isPlayerInMinAgroRnage)
            {
                stateMachine.ChangeState(elf.playerDetectedState);
            }
            else
            {
                stateMachine.ChangeState(elf.lookForPlayerState);
            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
