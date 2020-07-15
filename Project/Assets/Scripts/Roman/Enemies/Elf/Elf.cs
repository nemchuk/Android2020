using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elf : Entity
{
    public Elf_IdleState idleState { get; private set; }

    public Elf_MoveState moveState { get; private set; }

    public Elf_PlayerDetectedState playerDetectedState { get; private set; }

    public Elf_ChargeState chargeState { get; private set; }

    public Elf_LookForPlayerState lookForPlayerState { get; private set; }

    public Elf_MeleeAttackState meleeAttackState { get; private set; }

    public Elf_StunState stunState { get; private set; }

    public Elf_DeadState deadState { get; private set; }


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
    private D_StunState stunStateData;
    [SerializeField]
    private D_DeadState deadStateData;


    [SerializeField]
    private Transform meleeAttackPosition;

    public override void Start()
    {
        base.Start();

         moveState = new Elf_MoveState(this, stateMachine, "move", moveStateData, this);

        idleState = new Elf_IdleState(this, stateMachine, "idle", idleStateData, this);

        playerDetectedState = new Elf_PlayerDetectedState(this, stateMachine, "playerDetected", playerDetectedStateData, this);

        chargeState = new Elf_ChargeState(this, stateMachine, "charge", chargeStateData, this);

        lookForPlayerState = new Elf_LookForPlayerState(this, stateMachine, "look", lookForPlayerStateData, this);

        meleeAttackState = new Elf_MeleeAttackState(this, stateMachine, "attack", meleeAttackPosition, meleeAttackStateData, this);

        stunState = new Elf_StunState(this, stateMachine, "stun", stunStateData, this);

        deadState = new Elf_DeadState(this, stateMachine, "dead", deadStateData, this);

        stateMachine.Initialize(moveState);

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
        }else
        if(! CheckPlayerInMinAgroRange() )
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
