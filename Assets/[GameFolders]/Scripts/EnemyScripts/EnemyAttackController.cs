using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackController : MonoBehaviour
{
    private float currentDamage;
    private EnemyAnimationController animController;
    public EnemyAnimationController AnimController { get { return (animController == null) ? animController = GetComponent<EnemyAnimationController>() : animController; } }
    private GameObject enemyTarget;
    public void SetAttackData(float attackDamage)
    {
        currentDamage = attackDamage;
    }
    public void Attack(bool status)
    {
        AnimController.AttackAnimation(status);
    }
    public void GiveDamage()
    {
        enemyTarget.GetComponent<IDamagable>().TakeDamage(currentDamage);
    }
    public void SetTarget(GameObject target)
    {
        enemyTarget = target;
    }
    public bool IsEnemyInRange()
    {
        float distance = Vector3.Distance(transform.position, enemyTarget.transform.position);
        if (distance <= 1)
            return true;
        else
            return false;
    }
}
