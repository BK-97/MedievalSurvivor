using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : BaseState
{
    public override void EnterState(StateController stateController)
    {
        stateController.AttackController.AttackState();
        stateController.AnimController.AttackAnimation(true);
    }

    public override void ExitState(StateController stateController, BaseState nextState)
    {
        stateController.AnimController.AttackAnimation(false);
        stateController.SwitchState(nextState);
    }

    public override void UpdateState(StateController stateController)
    {
        stateController.AttackController.AttackState();
        stateController.AnimController.AttackAnimation(true);

    }
    public override void FixedUpdateState(StateController stateController)
    {
        throw new System.NotImplementedException();
    }
}
