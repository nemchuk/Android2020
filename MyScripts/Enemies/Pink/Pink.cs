using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pink : Entity
{
    public Pink_IdleState idleState { get; private set; }

    public Pink_MoveState moveState { get; private set; }

    public Pink_PlayerDetectedState playerDetectedState { get; private set; }

    public Pink_ChargeState chargeState { get; private set; }

    public Pink_LookForPlayerState lookForPlayerState { get; private set; }

    public Pink_MeleeAttackState meleeAttackState { get; private set; }

    public Pink_StunState stunState { get; private set; }

    public Pink_DeadState deadState { get; private set; }


    [SerializeField]
    private D_IdleState idleStateData;
    [SerializeField]
    private D_MoveState moveStateData;
    [SerializeField]
    private D_PlayerDetectedState playerDetectedStateData;
    [SerializeField]
    private D_ChargeState chargeStateData;
    [SerializeField]
    private D_LookForPlayerState lookForPlayerStateData;
    [SerializeField]
    private D_MeleeAttackState meleeAttackStateData;
    [SerializeField]
    public D_StunState stunStateData;
    [SerializeField]
    public D_DeadState deadStateData;

    [SerializeField]
    private Transform meleeAttackPosition;

    public override void Start()
    {
        base.Start();

        moveState = new Pink_MoveState( this , stateMachine , "move" , moveStateData , this );

        idleState = new Pink_IdleState(this, stateMachine, "idle", idleStateData, this);

        playerDetectedState = new Pink_PlayerDetectedState(this, stateMachine, "playerDetected", playerDetectedStateData, this);

        chargeState = new Pink_ChargeState(this, stateMachine, "charge", chargeStateData, this);

        lookForPlayerState = new Pink_LookForPlayerState(this, stateMachine, "lookForPlayer", lookForPlayerStateData, this);

        meleeAttackState = new Pink_MeleeAttackState( this , stateMachine , "meleeAttack" , meleeAttackPosition , meleeAttackStateData , this);

        stunState = new Pink_StunState( this , stateMachine , "stun" , stunStateData , this);

        deadState = new Pink_DeadState( this , stateMachine , "dead" , deadStateData , this );

      

        stateMachine.Initialize(moveState);
    }

    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Gizmos.DrawWireSphere(meleeAttackPosition.position , meleeAttackStateData.attackRadius);

    }

    public override void Damage(AttackDetails _attackDetails)
    {
        base.Damage(_attackDetails);

        if (isDead)
        {
            stateMachine.ChangeState(deadState);
        }
        else
        if (isStunned && stateMachine.currentState != stunState)
        {
            stateMachine.ChangeState(stunState);
        }
    }
}
