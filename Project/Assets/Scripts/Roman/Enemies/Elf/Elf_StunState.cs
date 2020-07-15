using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elf_StunState : StunState
{

    private Elf elf;

    public Elf_StunState(Entity _entity, FiniteStateMachine _stateMachine, string _animBoolName, D_StunState _stateData, Elf _elf) : base(_entity, _stateMachine, _animBoolName, _stateData)
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

        if (isStunTimeOver)
        {
            if (performCloseRangeAction)
            {
                stateMachine.ChangeState(elf.meleeAttackState);
            }
            else
            if (isPlayerInMinAgroRange)
            {
                stateMachine.ChangeState(elf.chargeState);
            }
            else
            {
                elf.lookForPlayerState.SetTurnImmediately(true);
                stateMachine.ChangeState(elf.lookForPlayerState);
            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
