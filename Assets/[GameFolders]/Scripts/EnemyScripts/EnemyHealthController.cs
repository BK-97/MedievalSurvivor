using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthController : MonoBehaviour,IDamagable
{
    private float currentHealth;
    [HideInInspector]
    public bool isDead;
    public Slider healthBar;
    public void Die()
    {
        if (isDead)
            return;

        isDead = true;
        healthBar.value = 0;
        healthBar.enabled = false;
        var go = MultiGameObjectPool.Instance.GetObject("SkeletonDeath");
        go.transform.position = transform.position;
        Destroy(gameObject);
    }

    public void SetHealth(float health)
    {
        currentHealth = health;
        healthBar.maxValue = health;
        healthBar.value = health;
        healthBar.enabled = true;
    }

    public void TakeDamage(float damage)
    {
        if (currentHealth - damage <= 0)
            Die();
        else
            currentHealth -= damage;

        healthBar.value = currentHealth;
    }
}
