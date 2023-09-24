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
        isDead = true;
        healthBar.value = currentHealth;
        healthBar.enabled = false;
        gameObject.SetActive(false);
        MultiGameObjectPool.Instance.ReturnObject(gameObject);
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
