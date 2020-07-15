public class Elf_MoveState : MoveState
{

    private Elf elf;

    public Elf_MoveState(Entity _entity, FiniteStateMachine _stateMachine, string _animBoolName, D_MoveState _stateData, Elf _elf) : base(_entity, _stateMachine, _animBoolName, _stateData)
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

        if (isPlayerInMinAgroRange)
        {
            stateMachine.ChangeState(elf.playerDetectedState);
        }

        else if (isDetectingWall || !isDetectingLedge)
        {
            elf.idleState.SetFlip(true);
            stateMachine.ChangeState(elf.idleState);
        }

    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
