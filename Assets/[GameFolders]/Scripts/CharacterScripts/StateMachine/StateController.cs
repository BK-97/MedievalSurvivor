using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterMovementController))]
[RequireComponent(typeof(CharacterAttackController))]
[RequireComponent(typeof(CharacterHealthController))]
public class StateController : MonoBehaviour
{
    #region StateParams
    public BaseState currentState = null;
    public IdleState idleState = new IdleState();
    public MoveState moveState = new MoveState();
    public AttackState attackState = new AttackState();
    #endregion
    #region Data
    public CharacterData characterData;
    #endregion
    #region Controllers
    private CharacterMovementController movementController;
    public CharacterMovementController MovementController { get { return (movementController == null) ? movementController = GetComponent<CharacterMovementController>() : movementController; } }
    private CharacterAttackController attackController;
    public CharacterAttackController AttackController { get { return (attackController == null) ? attackController = GetComponent<CharacterAttackController>() : attackController; } }
    private CharacterHealthController healthController;
    public CharacterHealthController HealthController { get { return (healthController == null) ? healthController = GetComponent<CharacterHealthController>() : healthController; } }
    private CharacterAnimationController animController;
    public CharacterAnimationController AnimController { get { return (animController == null) ? animController = GetComponentInChildren<CharacterAnimationController>() : animController; } }
    #endregion
    private void Start()
    {
        CharacterHealthController.OnHealthSet.Invoke(characterData.Health);
        AttackController.SetAttackData(characterData.BaseDamage);
        MovementController.SetSpeed(characterData.MoveSpeed);
        idleState.EnterState(this);
        currentState = idleState;
    }
    private void Update()
    {
        if (!LevelManager.Instance.IsLevelStarted)
            return;
        if (currentState == null)
            return;

        currentState.UpdateState(this);


        if (CheckAttackInput())
        {
            if(currentState!=attackState)
                currentState.ExitState(this, attackState);
            else
            {
                if (animController.IsInComboWindow())
                    animController.comboContinue=true;
            }
        }
        else
        {
            if (animController.comboContinue)
                return;

            if (currentState == attackState)
                currentState.ExitState(this, idleState);

        }
        if (CheckMovementInput())
        {
            if (currentState != moveState&&currentState!=attackState)
                currentState.ExitState(this, moveState);
        }
        else
        {
            if (currentState == moveState)
                currentState.ExitState(this, idleState);
        }

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
