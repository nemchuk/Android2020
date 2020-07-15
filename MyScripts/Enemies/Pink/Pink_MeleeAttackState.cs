using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pink_MeleeAttackState : MeleeAttackState
{

    private Pink pink;

    public Pink_MeleeAttackState(Entity _entity, FiniteStateMachine _stateMachine, string _animBoolName, Transform _attackPosition, D_MeleeAttackState _stateData,Pink _pink) : base(_entity, _stateMachine, _animBoolName, _attackPosition, _stateData)
    {
        pink = _pink;
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

    public override void FinishAttack()
    {
        base.FinishAttack();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if(isAnimationFinished)
        {
            if(isPlayerInMinAgroRange)
            {
                stateMachine.ChangeState(pink.playerDetectedState);
            }else
            {
                stateMachine.ChangeState(pink.lookForPlayerState);
            }
        }

    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void TriggerAttack()
    {
        base.TriggerAttack();
    }
}
