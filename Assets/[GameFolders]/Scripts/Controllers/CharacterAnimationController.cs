using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimationController : MonoBehaviour
{
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
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

    public bool IsPlayingAnimation(string animationName)
    {
        if (animator == null)
        {
            return false;
        }

        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

        return animator.GetCurrentAnimatorClipInfo(0)[0].clip.name == animationName && stateInfo.normalizedTime < 1f;
    }
}
