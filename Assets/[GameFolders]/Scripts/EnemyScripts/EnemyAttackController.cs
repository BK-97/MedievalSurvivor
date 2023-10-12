using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackController : MonoBehaviour
{
    #region Params
    private float currentDamage;
    private float attackRange;

    private GameObject enemyTarget;

    private bool canAttack;

    private EnemyAnimationController animController;
    public EnemyAnimationController AnimController { get { return (animController == null) ? animController = GetComponent<EnemyAnimationController>() : animController; } }
    #endregion
    #region AttackMethods
    public void Attack(bool status)
    {
        if (!canAttack)
            return;

        AnimController.AttackAnimation(status);
    }
    IEnumerator WaitAfterAttackCO(float waitTime)
    {
        canAttack = false;
        yield return new WaitForSeconds(waitTime);
        canAttack = true;
    }
    public void GiveDamage()
    {
        enemyTarget.GetComponent<IDamagable>().TakeDamage(currentDamage);
        AnimController.AttackAnimation(false);

        StartCoroutine(WaitAfterAttackCO(1));
    }
    public void GetHit()
    {
        WaitAfterAttackCO(0.5f);
    }

    public bool IsEnemyInRange()
    {
        if (enemyTarget == null)
            return false;
        float distance = Vector3.Distance(transform.position, enemyTarget.transform.position);
        return (distance <= attackRange + 0.1f);
    }
    #endregion
    #region GetSet
    public void SetAttackData(float attackDamage, float range)
    {
        currentDamage = attackDamage;
        attackRange = range;
        canAttack = true;
    }
    public void SetTarget(GameObject target)
    {
        enemyTarget = target;
    }
    #endregion
}
