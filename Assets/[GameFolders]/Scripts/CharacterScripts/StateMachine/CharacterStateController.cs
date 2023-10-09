using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterMovementController))]
[RequireComponent(typeof(CharacterAttackController))]
[RequireComponent(typeof(CharacterHealthController))]
[RequireComponent(typeof(SkillController))]
public class CharacterStateController : MonoBehaviour
{
    #region StateParams
    public BaseState currentState = null;
    public IdleState idleState = new IdleState();
    public MoveState moveState = new MoveState();
    public AttackState attackState = new AttackState();
    public SkillState skillState = new SkillState();
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
    private SkillController skillController;
    public SkillController SkillController { get { return (skillController == null) ? skillController = GetComponent<SkillController>() : skillController; } }
    private CharacterAnimationController animController;
    public CharacterAnimationController AnimController { get { return (animController == null) ? animController = GetComponent<CharacterAnimationController>() : animController; } }
    #endregion
    private void Awake()
    {
        CharacterHealthController.OnHealthSet.Invoke(characterData.Health);
        MovementController.Initialize(characterData.MoveSpeed);
        AttackController.SetAttackData(characterData.BaseDamage, characterData.AttackRange);
        idleState.EnterState(this);
        currentState = idleState;
    }
    private void OnEnable()
    {
        CharacterAnimationController.OnStartSkillAnim.AddListener(()=>SwitchState(skillState));
        InputManager.OnRollOverInput.AddListener(RollOver);
    }
    private void OnDisable()
    {
        CharacterAnimationController.OnStartSkillAnim.RemoveListener(() => SwitchState(skillState));
        InputManager.OnRollOverInput.RemoveListener(RollOver);

    }
    private void Update()
    {
        if (!LevelManager.Instance.IsLevelStarted)
            return;
        if (currentState == null)
            return;
        if (AnimController.IsRolling())
            return;

        currentState.UpdateState(this);

        if (currentState == idleState)
        {
            if (CheckAttackInput())
            {
                currentState.ExitState(this, attackState);
                return;
            }
            else if (CheckMovementInput())
            {
                currentState.ExitState(this, moveState);
                return;
            }
        }
        else if (currentState == moveState)
        {
            if (CheckAttackInput())
            {
                currentState.ExitState(this, attackState);
                return;
            }
            else if (!CheckMovementInput())
            {
                currentState.ExitState(this, idleState);
                return;
            }
        }
        else if (currentState == attackState)
        {
            if (AnimController.comboContinue)
                return;

            if (!AnimController.IsAttacking())
            {
                currentState.ExitState(this, idleState);
                return;
            }
        }
        else if (currentState == skillState)
        {
            if (!AnimController.IsOnSkillAnim())
            {
                currentState.ExitState(this, idleState);
                return;
            }
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
    private void RollOver()
    {
        if (MovementController.canRollOver&&!AnimController.IsRolling()&&!AnimController.IsAttacking())
        {
            MovementController.RollOver();
            AnimController.RollOver();
        }
    }
    #region CheckMethods
    public bool CheckMovementInput()
    {
        Vector3 movementDirection = InputManager.Instance.GetDirection();
        if (movementDirection == Vector3.zero)
        {
            return false;
        }
        else
            return true;
    }

    public bool CheckAttackInput()
    {
        bool isAttacking = InputManager.Instance.IsAttacking();
        return isAttacking;
    }

    #endregion
}
