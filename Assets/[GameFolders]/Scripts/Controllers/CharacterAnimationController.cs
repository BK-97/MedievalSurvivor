using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimationController : MonoBehaviour
{
    private Animator animator;
    public bool comboContinue;
    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    private void OnEnable()
    {
        SkillController.OnPassiveSkillUse.AddListener(PassiveSkillAnimation);
        SkillController.OnWeaponSkillUse.AddListener(WeaponSkillAnimation);
    }
    private void OnDisable()
    {
        SkillController.OnPassiveSkillUse.RemoveListener(PassiveSkillAnimation);
        SkillController.OnWeaponSkillUse.RemoveListener(WeaponSkillAnimation);
    }
    public void SetSpeed(float currentSpeed, float maxSpeed)
    {
        float normalizedSpeed = currentSpeed / maxSpeed;
        animator.SetFloat(AnimationKeys.SPEED, normalizedSpeed);

    }
    public void SetWeaponIndex(int weaponIndex)
    {
        animator.SetInteger(AnimationKeys.WEAPON_INDEX, weaponIndex);
    }
    public void AttackAnimation(bool status)
    {
        animator.SetBool(AnimationKeys.ATTACK_BOOL, status);

        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.4f && comboContinue)
        {
            comboContinue = false;
        }
    }

    private void PassiveSkillAnimation()
    {
        animator.SetTrigger(AnimationKeys.PASSIVE_SKILL);
    }
    private void WeaponSkillAnimation()
    {
        animator.SetTrigger(AnimationKeys.WEAPON_SKILL);
    }
    public bool IsInComboWindow()
    {
        float normalizedTime = animator.GetCurrentAnimatorStateInfo(0).normalizedTime; 
        return normalizedTime >= 0.5f && normalizedTime <= 0.9f;
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
