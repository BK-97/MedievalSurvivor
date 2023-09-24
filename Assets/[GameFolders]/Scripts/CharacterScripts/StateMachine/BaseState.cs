public abstract class BaseState
{
    public abstract void EnterState(CharacterStateController stateController);
    public abstract void UpdateState(CharacterStateController stateController);
    public abstract void FixedUpdateState(CharacterStateController stateController);
    public abstract void ExitState(CharacterStateController stateController, BaseState nextState);

}