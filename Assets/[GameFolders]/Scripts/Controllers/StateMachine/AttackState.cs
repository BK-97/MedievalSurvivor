using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : BaseState
{
    public override void EnterState(StateController stateController)
    {
        stateController.AttackController.AttackState();
    }

    public override void ExitState(StateController stateController, BaseState nextState)
    {
        stateController.SwitchState(nextState);
    }

    public override void UpdateState(StateController stateController)
    {
        stateController.AttackController.AttackState();

    }
    public override void FixedUpdateState(StateController stateController)
    {
        throw new System.NotImplementedException();
    }
}
