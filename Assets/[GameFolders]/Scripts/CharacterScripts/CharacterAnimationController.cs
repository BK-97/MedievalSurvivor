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
    private bool isRolling;
    [SerializeField]
    private bool isAttacking;
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
    private void FixedUpdate()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("SwordCombo2"))
        {
            AnimatorStateInfo animationState = animator.GetCurrentAnimatorStateInfo(0);
            AnimatorClipInfo[] myAnimatorClip = animator.GetCurrentAnimatorClipInfo(0);
            float myTime = myAnimatorClip[0].clip.length * animationState.normalizedTime;

            Debug.Log(myTime);

        }
        if (isRolling)
        {
            float animationTime = animator.GetCurrentAnimatorStateInfo(0).length / animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
            if (animationTime > 0.95f)
                isRolling = false;
        }
        if (isAttacking)
        {
            if (!comboContinue)
            {
                float animationTime = animator.GetCurrentAnimatorStateInfo(0).length * animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
                if (animationTime > 0.95f)
                    isAttacking = false;
            }
        }
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
        isRolling = true;
        animator.SetTrigger(AnimationKeys.ROLL_OVER);
    }
    #endregion
    #region AttackAnims
    public void SetWeaponIndex(int weaponIndex)
    {
        animator.SetInteger("WeaponIndex", weaponIndex);
    }
    public void AttackAnimation()
    {
        if (isAttacking)
        {
            float animationTime = animator.GetCurrentAnimatorStateInfo(0).length * animator.GetCurrentAnimatorStateInfo(0).normalizedTime;

            if (animationTime > 0.2f)
            {
                comboContinue = true;
            }
        }
        else
        {
            isAttacking = true;
            animator.SetBool(AnimationKeys.ATTACK_BOOL, true);
        }
    }
    
    public void AttackEndEvent()
    {
        if (comboContinue)
            return;
        else
            isAttacking = false;
    }
    public void EndAttack()
    {
        animator.SetBool(AnimationKeys.ATTACK_BOOL, false);
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
    public bool IsAttacking()
    {
        return isAttacking;
    }
    public bool IsRolling()
    {
        return isRolling;
    }
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
