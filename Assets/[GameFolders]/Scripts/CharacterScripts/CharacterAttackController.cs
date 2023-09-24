using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAttackController : MonoBehaviour
{
    float currentDamage;
    private CharacterAnimationController animController;
    public CharacterAnimationController AnimController { get { return (animController == null) ? animController = GetComponentInChildren<CharacterAnimationController>() : animController; } }
    private WeaponController weaponController;
    public WeaponController WeaponController { get { return (weaponController == null) ? weaponController = GetComponent<WeaponController>() : weaponController; } }
    public void SetAttackData(float damage)
    {
        currentDamage = damage;
        Debug.Log(currentDamage);
    }
    public void Attack(bool status)
    {
        Vector3 attackDirection = InputManager.Instance.GetMouseWorldPos()-transform.position;
        if (attackDirection != Vector3.zero)
        {
            Quaternion rotation = Quaternion.LookRotation(attackDirection);
            transform.rotation = rotation;
        }

        AnimController.AttackAnimation(status);
    }

    public void GiveDamage(IDamagable enemyTarget)
    {
        enemyTarget.TakeDamage(currentDamage);
    }
    public void AttackMoment()
    {
        if (WeaponController.CheckForContact())
            GiveDamage(WeaponController.GetTriggeredDamagable());
    }
}
