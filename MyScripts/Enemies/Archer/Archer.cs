using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer : Entity
{
   public Archer_MoveState moveState { get; private set; }

   public Archer_IdleState idleState { get; private set; }

    public Archer_PlayerDetectedState playerDetectedState { get; private set; }

    public Archer_MeleeAttackState meleeAttackState { get; private set; }

    public Archer_LookForPlayerState lookForPlayerState { get; private set; }

    public Archer_StunState stunState { get; private set; }

    public Archer_DeadState deadState { get; private set; }

    public Archer_DodgeState dodgeState { get; private set; }

    public Archer_RangeAttackState rangeAttackState { get; private set; }

    [SerializeField]
    private D_MoveState moveStateData;
    [SerializeField]
    private D_IdleState idleStateData;
    [SerializeField]
    private D_PlayerDetectedState playerDetectedStateData;
    [SerializeField]
    private D_MeleeAttackState meleeAttackStateData;
    [SerializeField]
    private D_LookForPlayerState lookForPlayerStateData;
    [SerializeField]
    private D_StunState stunStateData;
    [SerializeField]
    private D_DeadState deadStateData;
    [SerializeField]
    public D_DodgeState dodgeStateData;
    [SerializeField]
    private D_RangeAttackState rangeAttackStateData;

    [SerializeField]
    private Transform meleeAttackPosition;
    [SerializeField]
    private Transform rangeAttackPosition;




    public override void Start()
    {

        base.Start();

        moveState = new Archer_MoveState( this , stateMachine , "move" , moveStateData , this );

        idleState = new Archer_IdleState(this, stateMachine, "idle", idleStateData, this);

        playerDetectedState = new Archer_PlayerDetectedState(this, stateMachine, "playerDetected", playerDetectedStateData, this);

        meleeAttackState = new Archer_MeleeAttackState(this, stateMachine, "attack", meleeAttackPosition, meleeAttackStateData, this);

        lookForPlayerState = new Archer_LookForPlayerState(this, stateMachine, "look", lookForPlayerStateData, this);

        stunState = new Archer_StunState(this, stateMachine, "stun", stunStateData, this);

        deadState = new Archer_DeadState(this, stateMachine, "dead", deadStateData, this);

        dodgeState = new Archer_DodgeState(this, stateMachine, "dodge", dodgeStateData, this);

        rangeAttackState = new Archer_RangeAttackState(this , stateMachine , "range" , rangeAttackPosition , rangeAttackStateData , this);

        stateMachine.Initialize(moveState);
    }

    

    public override void Damage(AttackDetails _attackDetails)
    {
        base.Damage(_attackDetails);

        if (isDead)
        {
            stateMachine.ChangeState(deadState);
        }
        else if (isStunned && stateMachine.currentState != stunState)
        {
            stateMachine.ChangeState(stunState);
        }
        else if (CheckPlayerInMinAgroRange())
        {
            stateMachine.ChangeState(rangeAttackState);
        }
        else if (!CheckPlayerInMinAgroRange())
        {
            lookForPlayerState.SetTurnImmediately(true);
            stateMachine.ChangeState(lookForPlayerState);
        }
    }

    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Gizmos.DrawWireSphere(meleeAttackPosition.position, meleeAttackStateData.attackRadius);
    }
}
