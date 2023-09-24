public class SkillState : BaseState
{
    public override void EnterState(CharacterStateController stateController)
    {
    }

    public override void ExitState(CharacterStateController stateController, BaseState nextState)
    {
        stateController.SwitchState(nextState);
    }

    public override void FixedUpdateState(CharacterStateController stateController)
    {
    }

    public override void UpdateState(CharacterStateController stateController)
    {
    }
}
