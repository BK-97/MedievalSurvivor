using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationController : MonoBehaviour
{
    #region Params
    [HideInInspector]
    public Animator animator;
    private EnemyAttackController attackController;
    public EnemyAttackController AttackController { get { return (attackController == null) ? attackController = GetComponent<EnemyAttackController>() : attackController; } }
    #endregion
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    public void SetSpeed(float currentSpeed, float maxSpeed)
    {
        float normalizedSpeed = currentSpeed / maxSpeed;
        animator.SetFloat(AnimationKeys.SPEED, normalizedSpeed);
    }
    public void HitAnimation()
    {
        CancelAttackAnimation();
        GetComponent<EnemyMovementController>().BackStep();
        GetComponent<EnemyAttackController>().GetHit();
        animator.SetTrigger(AnimationKeys.GET_HIT_ANIMATION);
    }
    public void AttackAnimation(bool status)
    {
        animator.SetBool(AnimationKeys.ATTACK_BOOL, status);
    }
    public void CancelAttackAnimation()
    {
        if (animator.GetBool(AnimationKeys.ATTACK_BOOL))
        {
            animator.SetTrigger(AnimationKeys.CANCEL_ATTACK);
            animator.SetBool(AnimationKeys.ATTACK_BOOL, false);
        }
    }
    public void AttackEvent()
    {
        AttackController.GiveDamage();
    }
    public bool CanSwitchState()
    {
        bool isAttackPlaying = animator.GetCurrentAnimatorStateInfo(0).IsName("Attack");
        return isAttackPlaying;
    }
}
