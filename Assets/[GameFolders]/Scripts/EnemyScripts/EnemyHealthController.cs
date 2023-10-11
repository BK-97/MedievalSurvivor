using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class EnemyHealthController : MonoBehaviour, IDamagable
{
    private EnemyAnimationController animationController;
    private float currentHealth;
    [HideInInspector]
    public bool isDead;
    public Slider healthBar;
    public Slider fakeBar;
    public bool isBoss;
    public static UnityEvent OnBossDie = new UnityEvent();

    private void Start()
    {
        animationController = GetComponent<EnemyAnimationController>();
    }
    public void Die()
    {
        if (isDead)
            return;

        isDead = true;
        currentHealth = 0;
        if (!isBoss)
        {
            healthBar.value = 0;
            fakeBar.value = 0;
            healthBar.enabled = false;
            fakeBar.enabled = false;
            ObjectPoolManager.SpawnObject(ObjectPoolManager.Instance.GetObjectFromName("SkeletonDeathFX"), transform.position, Quaternion.identity);
            ObjectPoolManager.ReturnObjectToPool(gameObject);
        }
        else
        {
            animationController.BossDie();
            Invoke("BossDie",2);

        }

    }
    private void BossDie()
    {
        ObjectPoolManager.ReturnObjectToPool(gameObject);
        OnBossDie.Invoke();
    }
    public void SetHealth(float health)
    {
        currentHealth = health;
        if (isBoss)
            return;
        healthBar.maxValue = health;
        healthBar.value = health;
        healthBar.enabled = true;
        fakeBar.maxValue = health;
        fakeBar.value = health;
        fakeBar.enabled = true;
        isDead = false;
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
            currentHealth -= damage;
            if (isBoss)
                return;

            healthBar.value = currentHealth;
            animationController.HitAnimation();
            StartCoroutine(FakeBarWaitCO());
        }

    }
    public float GetCurrentHealth()
    {
        return currentHealth;
    }
}
