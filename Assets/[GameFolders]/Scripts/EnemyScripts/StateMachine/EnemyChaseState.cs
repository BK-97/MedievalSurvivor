public class EnemyChaseState : EnemyBaseState
{
    public override void EnterState(EnemyStateController stateController)
    {
        stateController.MovementController.Move();
    }

    public override void ExitState(EnemyStateController stateController, EnemyBaseState nextState)
    {
        stateController.MovementController.MoveEnd();
        stateController.SwitchState(nextState);

    }

    public override void UpdateState(EnemyStateController stateController)
    {
        stateController.MovementController.Move();
        if (stateController.AttackController.IsEnemyInRange())
            ExitState(stateController, stateController.attackState);
    }
}
