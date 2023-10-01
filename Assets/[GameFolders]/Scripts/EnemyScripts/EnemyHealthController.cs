using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthController : MonoBehaviour,IDamagable
{
    private EnemyAnimationController animationController;
    private float currentHealth;
    [HideInInspector]
    public bool isDead;
    public Slider healthBar;
    public Slider fakeBar;
    private void Start()
    {
        animationController = GetComponent<EnemyAnimationController>();
    }
    public void Die()
    {
        if (isDead)
            return;

        isDead = true;
        healthBar.value = 0;
        fakeBar.value = 0;
        healthBar.enabled = false;
        fakeBar.enabled = false;
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
        fakeBar.maxValue = health;
        fakeBar.value = health;
        fakeBar.enabled = true;

    }
    IEnumerator FakeBarWaitCO()
    {
        yield return new WaitForSeconds(0.3f);
        fakeBar.value = currentHealth;
    }

    public void TakeDamage(float damage)
    {
        if (currentHealth - damage <= 0)
            Die();
        else
        {
            animationController.HitAnimation();
            currentHealth -= damage;
            healthBar.value = currentHealth;
            StartCoroutine(FakeBarWaitCO());
        }

    }
}
