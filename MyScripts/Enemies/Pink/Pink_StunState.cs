using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pink_StunState : StunState
{

    private Pink pink;

    public Pink_StunState(Entity _entity, FiniteStateMachine _stateMachine, string _animBoolName, D_StunState _stateData,Pink _pink) : base(_entity, _stateMachine, _animBoolName, _stateData)
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

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if(isStunTimeOver)
        {
            if(performCloseRangeAction)
            {
                stateMachine.ChangeState(pink.meleeAttackState);
            }
            else
            if(isPlayerInMinAgroRange)
            {
                stateMachine.ChangeState(pink.chargeState);
            }
            else
            {
                pink.lookForPlayerState.SetTurnImmediately(true);
                stateMachine.ChangeState(pink.lookForPlayerState);
            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
