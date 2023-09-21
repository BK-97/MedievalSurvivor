using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateController : MonoBehaviour
{
    #region StateParams
    public BaseState currentState = null;
    public IdleState idleState = new IdleState();
    public MoveState moveState = new MoveState();
    public AttackState attackState = new AttackState();
    #endregion
    #region Controllers
    public CharacterMovementController MovementController;
    public CharacterAttackController AttackController;

    #endregion

    private void Start()
    {
        idleState.EnterState(this);
        currentState = idleState;
    }
    private void Update()
    {
        if (!LevelManager.Instance.IsLevelStarted)
            return;
        if (currentState == null)
            return;
        if (CheckAttackInput())
            currentState.ExitState(this,attackState);
        else if (CheckMovementInput())
            currentState.ExitState(this, moveState);
        else
            currentState.ExitState(this, idleState);
        currentState.UpdateState(this);
    }
    private void FixedUpdate()
    {
        if (currentState == null)
            return;
        if (currentState == moveState)
            currentState.FixedUpdateState(this);
    }
    public void SwitchState(BaseState changeState)
    {
        if (currentState == changeState)
            return;
        currentState = changeState;
        currentState.EnterState(this);
    }
    
    #region CheckMethods
    private bool CheckMovementInput()
    {
        Vector3 movementDirection = InputManager.Instance.GetDirection();
        if (movementDirection == Vector3.zero)
        {
            return false;
        }
        else
            return true;
    }
    private bool CheckAttackInput()
    {
        bool isAttacking = InputManager.Instance.IsAttacking();
        return isAttacking;
    }
    #endregion
}
