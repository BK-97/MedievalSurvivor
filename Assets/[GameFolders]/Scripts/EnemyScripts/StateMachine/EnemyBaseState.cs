public abstract class EnemyBaseState
{
    public abstract void EnterState(EnemyStateController stateController);
    public abstract void UpdateState(EnemyStateController stateController);
    public abstract void ExitState(EnemyStateController stateController, EnemyBaseState nextState);

}