using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CharacterAnimationController : MonoBehaviour
{
    #region Params
    public Animator animator;
    public bool comboContinue;
    private int comboIndex;
    private SkillController skillController;
    private CharacterAttackController attackController;
    #endregion
    #region Events
    public static UnityEvent OnStartSkillAnim = new UnityEvent();
    #endregion
    #region MonoMethods
    private void Start()
    {
        skillController = GetComponent<SkillController>();
        attackController = GetComponent<CharacterAttackController>();
    }
    private void OnEnable()
    {
        SkillController.OnPassiveSkillUse.AddListener(PassiveSkillAnimation);
        SkillController.OnWeaponSkillUse.AddListener(WeaponSkillAnimation);
        CharacterHealthController.OnCharacterDie.AddListener(DeathAnim);
    }
    private void OnDisable()
    {
        SkillController.OnPassiveSkillUse.RemoveListener(PassiveSkillAnimation);
        SkillController.OnWeaponSkillUse.RemoveListener(WeaponSkillAnimation);
        CharacterHealthController.OnCharacterDie.RemoveListener(DeathAnim);

    }
    #endregion
    #region MoveAnims
    public void SetSpeed(float currentSpeed, float maxSpeed)
    {
        float normalizedSpeed = currentSpeed / maxSpeed;
        animator.SetFloat(AnimationKeys.SPEED, normalizedSpeed);
        if (currentSpeed == 0)
            animator.applyRootMotion = true;
        else
            animator.applyRootMotion = false;
    }
    public void RollOver()
    {
        animator.applyRootMotion = true;
        animator.SetTrigger(AnimationKeys.ROLL_OVER);
    }
    #endregion
    #region AttackAnims
    public void SetWeaponIndex(int weaponIndex)
    {
        animator.SetInteger("WeaponIndex", weaponIndex);
    }
    public void AttackAnimation(bool status)
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
        {
            float animationTime = animator.GetCurrentAnimatorStateInfo(0).length * animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
            if (animationTime < 0.3f && comboContinue)
            {
                comboContinue = false;
            }
            if(animationTime > 0.3f)
            {
                if (status)
                    comboContinue = true;
            }

        }
        if (comboContinue)
            status = true;
        
        animator.SetBool(AnimationKeys.ATTACK_BOOL, status);
    }
    public void AttackEvent()
    {
        attackController.AttackMoment();
    }
    #endregion
    #region SkillAnims
    private void PassiveSkillAnimation()
    {
        animator.SetTrigger(AnimationKeys.PASSIVE_SKILL);
        OnStartSkillAnim.Invoke();
    }
    private void WeaponSkillAnimation()
    {
        animator.SetTrigger(AnimationKeys.WEAPON_SKILL);
        OnStartSkillAnim.Invoke();
    }
    public void WeaponSkillEvent()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, skillController.weaponSkillRadius, LayerMask.GetMask("Enemy"));
        var go = MultiGameObjectPool.Instance.GetObject("RockHitGround");
        go.transform.position = transform.position;
        foreach (Collider collider in hitColliders)
        {
            IDamagable damagable = collider.GetComponent<IDamagable>();
            attackController.GiveDamage(damagable, 40);
        }
    }
    #endregion
    #region DeathAnim
    private void DeathAnim()
    {
        animator.Rebind();
        animator.SetTrigger(AnimationKeys.DIE);
    }
    #endregion
    #region Helpers
    public bool IsInComboWindow()
    {
        float normalizedTime = animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
        return normalizedTime >= 0.5f && normalizedTime <= 0.9f;
    }
    public bool GetAnimStatus(string animName)
    {
        return animator.GetCurrentAnimatorStateInfo(0).IsName(animName);
    }
    public float GetCurrentAnimTime()
    {
        return animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
    }
    #endregion

}
