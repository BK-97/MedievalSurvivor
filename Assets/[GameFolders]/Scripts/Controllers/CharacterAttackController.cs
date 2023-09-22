using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAttackController : MonoBehaviour
{
    float currentDamage;
    public void SetAttackData(float damage)
    {
        currentDamage = damage;
    }
    bool isAttacking;
    public void AttackState()
    {
        isAttacking = true;
    }
    public void AttackEnd()
    {
        isAttacking = false;
    }
}
