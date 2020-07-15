using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeAttackState : AttackState
{

    protected D_RangeAttackState stateData;

    protected GameObject projectile;
    protected Arrow arrowScript;

    public RangeAttackState(Entity _entity, FiniteStateMachine _stateMachine, string _animBoolName, Transform _attackPosition,D_RangeAttackState _stateData) : base(_entity, _stateMachine, _animBoolName, _attackPosition)
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
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void TriggerAttack()
    {
        base.TriggerAttack();

        projectile = GameObject.Instantiate(stateData.projectile , attackPosition.position , attackPosition.rotation);

        arrowScript = projectile.GetComponent<Arrow>();

        arrowScript.FireProjectile(stateData.projectileSpeed , stateData.projectileTravelDistance , stateData.projectileDamage);
    }
}
