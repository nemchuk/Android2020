using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//When a player is detected,we want to wait a certain amount of time and than charge at the enemy.
//When time is up, there 2 sorts of possible actions.Charge at the player(if he is minn agro ragne) and to attack (in max agro range)

public class PlayerDetectedState : State
{

    protected D_PlayerDetectedState stateData;

    protected bool isPlayerInMinAgroRange;
    protected bool isPlayerInMaxAgroRange;

    protected bool performLongRangeAction;
    protected bool performCloseRangeAction;

    public PlayerDetectedState(Entity _entity, FiniteStateMachine _stateMachine, string _animBoolName , D_PlayerDetectedState _stateData) : base(_entity, _stateMachine, _animBoolName)
    {
        stateData = _stateData;
    }

    public override void DoChecks()
    {
        base.DoChecks();

        isPlayerInMinAgroRange = entity.CheckPlayerInMinAgroRange();
        isPlayerInMaxAgroRange = entity.CheckPlayerInMaxAgroRange();

        performCloseRangeAction = entity.CheckPlayerForCloseRangeAction();

    }

    public override void Enter()
    {
        base.Enter();

        performLongRangeAction = false;

        entity.SetVelocity(0);

    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if(Time.time >= startTime + stateData.longRangeActionTime)
        {
            performLongRangeAction = true;
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

    }
}
