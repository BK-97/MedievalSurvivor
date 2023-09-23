using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationController : MonoBehaviour
{
    [HideInInspector]
    public Animator animator;
    private EnemyAttackController attackController;
    private void Start()
    {
        animator = GetComponent<Animator>();
        attackController = GetComponent<EnemyAttackController>();
    }
    public void SetSpeed(float currentSpeed, float maxSpeed)
    {
        float normalizedSpeed = currentSpeed / maxSpeed;
        animator.SetFloat(AnimationKeys.SPEED, normalizedSpeed);

    }
    public void AttackAnimation(bool status)
    {
        animator.SetBool(AnimationKeys.ATTACK_BOOL, status);
    }
    public void CancelAttackAnimation()
    {
        animator.SetTrigger(AnimationKeys.CANCEL_ATTACK);
    }
    public void AttackEvent()
    {
        attackController.GiveDamage();
    }
    public bool CanSwitchState()
    {
        bool isAttackPlaying = animator.GetCurrentAnimatorStateInfo(0).IsName("Attack");
        return isAttackPlaying;
    }
}
