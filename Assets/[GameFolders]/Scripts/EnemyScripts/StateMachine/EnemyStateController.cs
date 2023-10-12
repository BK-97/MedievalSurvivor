using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyMovementController))]
[RequireComponent(typeof(EnemyAttackController))]
[RequireComponent(typeof(EnemyHealthController))]
public class EnemyStateController : MonoBehaviour
{
    #region StateParams
    public EnemyBaseState currentState = null;
    public EnemyIdleState idleState = new EnemyIdleState();
    public EnemyChaseState chaseState = new EnemyChaseState();
    public EnemyAttackState attackState = new EnemyAttackState();
    #endregion
    #region Data
    public CharacterData characterData;
    #endregion
    #region Controllers
    private EnemyMovementController movementController;
    public EnemyMovementController MovementController { get { return (movementController == null) ? movementController = GetComponent<EnemyMovementController>() : movementController; } }
    private EnemyAttackController attackController;
    public EnemyAttackController AttackController { get { return (attackController == null) ? attackController = GetComponent<EnemyAttackController>() : attackController; } }
    private EnemyHealthController healthController;
    public EnemyHealthController HealthController { get { return (healthController == null) ? healthController = GetComponent<EnemyHealthController>() : healthController; } }
    private EnemyAnimationController animController;
    public EnemyAnimationController AnimController { get { return (animController == null) ? animController = GetComponent<EnemyAnimationController>() : animController; } }
    #endregion
    private Transform targetTransform;

    public void Initialize()
    {
        GetComponent<Collider>().enabled = true;
        GetComponent<Rigidbody>().useGravity = true;
        HealthController.SetHealth(characterData.Health);
        AttackController.SetAttackData(characterData.BaseDamage,characterData.AttackRange);
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
        if (GameManager.Instance.IsStageCompleted)
            return;
        if (HealthController.isDead)
            return;

        currentState.UpdateState(this);
        AnimController.SetSpeed(MovementController.GetCurrentSpeed(),characterData.MoveSpeed);
    }
    public void SwitchState(EnemyBaseState changeState)
    {
        if (currentState == changeState)
            return;
        currentState = changeState;
        currentState.EnterState(this);
    }
    public void SetTarget(Transform target)
    {
        targetTransform = target;
        MovementController.SetTargetTransform(targetTransform);
        AttackController.SetTarget(targetTransform.gameObject);
    }
}
