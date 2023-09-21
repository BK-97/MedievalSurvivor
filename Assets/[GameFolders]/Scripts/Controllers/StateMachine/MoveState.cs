using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveState : BaseState
{
    public override void EnterState(StateController stateController)
    {
        stateController.MovementController.Move();

    }

    public override void ExitState(StateController stateController, BaseState nextState)
    {
        stateController.MovementController.Move();
        stateController.SwitchState(nextState);
    }

    public override void UpdateState(StateController stateController)
    {

    }
    public override void FixedUpdateState(StateController stateController)
    {
        stateController.MovementController.Move();
    }
}
