using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthController : MonoBehaviour,IDamagable
{
    private float currentHealth;
    [HideInInspector]
    public bool isDead;
    public void Die()
    {
        isDead = true;
        Destroy(gameObject);
    }

    public void SetHealth(float health)
    {
        currentHealth = health;

        Invoke("Die", 1);
    }

    public void TakeDamage(float damage)
    {
        if (currentHealth - damage <= 0)
            Die();
        else
            currentHealth -= damage;
    }

}
