using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveState : BaseState
{
    public override void EnterState(CharacterStateController stateController)
    {
        stateController.MovementController.RotateTowards();
        stateController.MovementController.Move();
    }

    public override void ExitState(CharacterStateController stateController, BaseState nextState)
    {
        stateController.MovementController.RotateTowards();
        stateController.MovementController.MoveEnd();
        stateController.SwitchState(nextState);
    }

    public override void UpdateState(CharacterStateController stateController)
    {
        stateController.MovementController.RotateTowards();

    }
    public override void FixedUpdateState(CharacterStateController stateController)
    {
        stateController.MovementController.Move();
    }
}
