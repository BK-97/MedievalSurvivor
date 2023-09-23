public abstract class BaseState
{
    public abstract void EnterState(StateController stateController);
    public abstract void UpdateState(StateController stateController);
    public abstract void FixedUpdateState(StateController stateController);
    public abstract void ExitState(StateController stateController, BaseState nextState);

}