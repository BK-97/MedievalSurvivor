using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : BaseState
{
    public override void EnterState(CharacterStateController stateController)
    {
    }

    public override void ExitState(CharacterStateController stateController, BaseState nextState)
    {
        stateController.SwitchState(nextState);
    }

    public override void UpdateState(CharacterStateController stateController)
    {

    }
    public override void FixedUpdateState(CharacterStateController stateController)
    {

    }
}
