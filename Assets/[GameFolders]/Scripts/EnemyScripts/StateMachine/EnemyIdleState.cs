public class EnemyIdleState : EnemyBaseState
{
    public override void EnterState(EnemyStateController stateController)
    {
    }

    public override void ExitState(EnemyStateController stateController, EnemyBaseState nextState)
    {
        stateController.SwitchState(nextState);
    }

    public override void UpdateState(EnemyStateController stateController)
    {
        if (LevelManager.Instance.IsLevelStarted)
            ExitState(stateController, stateController.chaseState);
    }
}
