public class EnemyAttackState : EnemyBaseState
{
    public override void EnterState(EnemyStateController stateController)
    {
        stateController.AttackController.Attack(true);
    }

    public override void ExitState(EnemyStateController stateController, EnemyBaseState nextState)
    {
        stateController.AttackController.Attack(false);
        stateController.SwitchState(nextState);

    }

    public override void UpdateState(EnemyStateController stateController)
    {
        stateController.AttackController.Attack(true);
        if (!stateController.AttackController.IsEnemyInRange())
            ExitState(stateController, stateController.chaseState);
    }
}
