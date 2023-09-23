using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackController : MonoBehaviour
{
    private float currentDamage;
    private EnemyAnimationController animController;
    public EnemyAnimationController AnimController { get { return (animController == null) ? animController = GetComponent<EnemyAnimationController>() : animController; } }
    private GameObject enemyTarget;
    private bool canAttack;
    public void SetAttackData(float attackDamage)
    {
        currentDamage = attackDamage;
        canAttack = true;
    }
    public void Attack(bool status)
    {
        if (!canAttack)
            return;

        canAttack = false;
        AnimController.AttackAnimation(status);

    }
    IEnumerator WaitAfterAttackCO()
    {
        yield return new WaitForSeconds(1);
        canAttack = true;
    }
    public void GiveDamage()
    {
        enemyTarget.GetComponent<IDamagable>().TakeDamage(currentDamage);
        AnimController.AttackAnimation(false);

        StartCoroutine(WaitAfterAttackCO());
    }
    public void SetTarget(GameObject target)
    {
        enemyTarget = target;
    }
    public bool IsEnemyInRange()
    {
        float distance = Vector3.Distance(transform.position, enemyTarget.transform.position);
        return (distance <= 1.1f);
    }
}
