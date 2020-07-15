using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer_DodgeState : Dodge_State
{

    private Archer archer;

    public Archer_DodgeState(Entity _entity, FiniteStateMachine _stateMachine, string _animBoolName, D_DodgeState _stateData, Archer _archer) : base(_entity, _stateMachine, _animBoolName, _stateData)
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

        if(isDodgeOver)
        {
            if(isPlayerInMaxAgroRange && performCloseRangeAction)
            {
                stateMachine.ChangeState(archer.meleeAttackState);
            }
            else if (isPlayerInMaxAgroRange && !performCloseRangeAction)
            {
                stateMachine.ChangeState(archer.rangeAttackState);
            }
            else
            if(!isPlayerInMaxAgroRange)
            {
                stateMachine.ChangeState(archer.lookForPlayerState);
            }
            
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
