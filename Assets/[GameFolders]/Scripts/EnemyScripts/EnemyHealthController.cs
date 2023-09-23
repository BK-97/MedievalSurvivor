using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthController : MonoBehaviour,IDamagable
{
    private float currentHealth;
    public void Die()
    {
        Destroy(gameObject);
    }

    public void SetHealth(float health)
    {
        currentHealth = health;
    }

    public void TakeDamage(float damage)
    {
        if (currentHealth - damage <= 0)
            Die();
        else
            currentHealth -= damage;
    }

}
