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
    private bool onAttackingAnim;
    private bool onSkillAnim;
    public bool canTakeCombo;
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
        animator.SetFloat(AnimationKeys.SPEED, 0);
        animator.SetBool(AnimationKeys.ROLL_OVER, true);

        animator.applyRootMotion = true;
        isRolling = true;
    }
    public void RollOverEnd()
    {
        animator.applyRootMotion = false;
        isRolling = false;
        animator.SetBool(AnimationKeys.ROLL_OVER, false);
    }
    #endregion
    #region AttackAnims
    public void SetWeaponIndex(int weaponIndex)
    {
        animator.SetInteger("WeaponIndex", weaponIndex);
    }
    public void AttackAnimation()
    {

        onAttackingAnim = true;
        animator.SetBool(AnimationKeys.ATTACK_BOOL, true);

    }
    public void ComboAttack()
    {
        comboContinue = true;
    }
    public void AttackStartEvent()
    {
        comboContinue = false;
        canTakeCombo = true;
    }
    public void AttackEvent()
    {
        attackController.AttackMoment();
    }
    public void AttackEndEvent()
    {
        onAttackingAnim = false;
        canTakeCombo = false;
    }
    public void EndAttack()
    {
        if(!comboContinue)
            animator.SetBool(AnimationKeys.ATTACK_BOOL, false);
    }

    #endregion
    #region SkillAnims
    private void PassiveSkillAnimation()
    {
        animator.SetTrigger(AnimationKeys.PASSIVE_SKILL);
        onSkillAnim = true;
        OnStartSkillAnim.Invoke();
    }
    private void WeaponSkillAnimation()
    {
        animator.SetTrigger(AnimationKeys.WEAPON_SKILL);
        onSkillAnim = true;
        OnStartSkillAnim.Invoke();
    }
    public void SkillAnimEnd() //Animation Event Method
    {
        onSkillAnim = false;
    }
    public void WeaponSkillEvent()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, skillController.weaponSkillRadius, LayerMask.GetMask("Enemy"));
        var go = ObjectPoolManager.SpawnObject(ObjectPoolManager.Instance.GetObjectFromName("RockHitGround"),transform.position,transform.rotation);
        go.transform.position = transform.position;
        foreach (Collider collider in hitColliders)
        {
            IDamagable damagable = collider.GetComponent<IDamagable>();
            attackController.GiveDamage(damagable, attackController.GetCurrentDamage()*2);
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
        return onAttackingAnim;
    }
    public bool IsOnSkillAnim()
    {
        return onSkillAnim;
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
