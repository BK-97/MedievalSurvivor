using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : BaseState
{
    public override void EnterState(StateController stateController)
    {
    }

    public override void ExitState(StateController stateController, BaseState nextState)
    {
        stateController.SwitchState(nextState);
    }

    public override void UpdateState(StateController stateController)
    {

    }
    public override void FixedUpdateState(StateController stateController)
    {

    }
}
