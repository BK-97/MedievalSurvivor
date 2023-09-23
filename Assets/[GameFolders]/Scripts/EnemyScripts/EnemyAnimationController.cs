using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationController : MonoBehaviour
{
    private Animator animator;
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
    public void AttackEvent()
    {
        attackController.GiveDamage();
    }
}
