using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer_StunState : StunState
{

    private Archer archer;

    public Archer_StunState(Entity _entity, FiniteStateMachine _stateMachine, string _animBoolName, D_StunState _stateData, Archer _archer) : base(_entity, _stateMachine, _animBoolName, _stateData)
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

        if (isStunTimeOver)
        {
            if (performCloseRangeAction)
            {
                stateMachine.ChangeState(archer.meleeAttackState);
            }
            else
            if (isPlayerInMinAgroRange)
            {
                stateMachine.ChangeState(archer.playerDetectedState);
            }
            else
            {
                archer.lookForPlayerState.SetTurnImmediately(true);
                stateMachine.ChangeState(archer.lookForPlayerState);
            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
