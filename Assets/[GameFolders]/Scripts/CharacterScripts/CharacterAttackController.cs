using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAttackController : MonoBehaviour
{
    float currentDamage;
    private CharacterAnimationController animController;
    public CharacterAnimationController AnimController { get { return (animController == null) ? animController = GetComponentInChildren<CharacterAnimationController>() : animController; } }
    public void SetAttackData(float damage)
    {
        currentDamage = damage;
    }

    public void Attack(bool status)
    {
        AnimController.AttackAnimation(status);
    }
    public void GiveDamage(IDamagable enemyTarget)
    {
        enemyTarget.TakeDamage(currentDamage);
    }
}
