using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class EnemyHealthController : MonoBehaviour, IDamagable
{
    #region Params
    private EnemyAnimationController animController;
    public EnemyAnimationController AnimController { get { return (animController == null) ? animController = GetComponent<EnemyAnimationController>() : animController; } }
    private float currentHealth;

    [HideInInspector]
    public bool isDead;

    public bool isBoss;

    public Slider healthBar;
    public Slider fakeBar;

    #endregion
    #region Events
    public static UnityEvent OnBossDie = new UnityEvent();
    #endregion
    #region IDamagableMethods
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
            AnimController.HitAnimation();
            StartCoroutine(FakeBarWaitCO());
        }

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
            AnimController.BossDie();
            Invoke("BossDie",2);

        }

    }
    #endregion
    private void BossDie()
    {
        ObjectPoolManager.ReturnObjectToPool(gameObject);
        OnBossDie.Invoke();
    }
  
    IEnumerator FakeBarWaitCO()
    {
        yield return new WaitForSeconds(0.3f);
        fakeBar.value = currentHealth;
    }

   
    public float GetCurrentHealth()
    {
        return currentHealth;
    }
}
