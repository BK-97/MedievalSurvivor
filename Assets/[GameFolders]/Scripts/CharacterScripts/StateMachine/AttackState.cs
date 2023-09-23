public class AttackState : BaseState
{
    public override void EnterState(StateController stateController)
    {
        stateController.AttackController.Attack(true);
    }

    public override void ExitState(StateController stateController, BaseState nextState)
    {
        stateController.AttackController.Attack(false);
        stateController.SwitchState(nextState);
    }

    public override void UpdateState(StateController stateController)
    {
        stateController.AttackController.Attack(true);
    }
    public override void FixedUpdateState(StateController stateController)
    {

    }
}
