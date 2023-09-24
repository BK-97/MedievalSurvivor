public class AttackState : BaseState
{
    public override void EnterState(CharacterStateController stateController)
    {
        stateController.AttackController.Attack(true);
    }

    public override void ExitState(CharacterStateController stateController, BaseState nextState)
    {
        stateController.AttackController.Attack(false);
        stateController.SwitchState(nextState);
    }

    public override void UpdateState(CharacterStateController stateController)
    {
        stateController.AttackController.Attack(InputManager.Instance.IsAttacking());
    }
    public override void FixedUpdateState(CharacterStateController stateController)
    {

    }
}
