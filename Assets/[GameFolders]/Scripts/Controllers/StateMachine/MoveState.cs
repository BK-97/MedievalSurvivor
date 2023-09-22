using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveState : BaseState
{
    public override void EnterState(StateController stateController)
    {
        stateController.MovementController.Move();
        stateController.MovementController.RotateTowards();

    }

    public override void ExitState(StateController stateController, BaseState nextState)
    {
        stateController.MovementController.MoveEnd();
        stateController.MovementController.RotateTowards();
        stateController.SwitchState(nextState);
    }

    public override void UpdateState(StateController stateController)
    {
        stateController.MovementController.RotateTowards();
    }
    public override void FixedUpdateState(StateController stateController)
    {
        stateController.MovementController.Move();
    }
}
