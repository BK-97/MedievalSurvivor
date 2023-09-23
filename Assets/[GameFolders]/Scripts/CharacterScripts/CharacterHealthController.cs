using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CharacterHealthController : MonoBehaviour, IDamagable
{
    private float currentHealth;
    bool canTakeDamage;
    public static FloatEvent OnHealthChange = new FloatEvent();
    public static FloatEvent OnHealthSet = new FloatEvent();
    public void SetHealth(float health)
    {
        currentHealth = health;
        canTakeDamage = true;
    }

    public void TakeDamage(float damage)
    {
        if (!canTakeDamage)
            return;
        if (currentHealth - damage <= 0)
            Die();
        else
            currentHealth -= damage;
        OnHealthChange.Invoke(currentHealth);

    }
    private void OnEnable()
    {
        OnHealthSet.AddListener(SetHealth);

        SkillController.OnPassiveSkillUse.AddListener(() => canTakeDamage = false);
        SkillController.OnPassiveSkillEnd.AddListener(() => canTakeDamage = true);
    }
    private void OnDisable()
    {
        OnHealthSet.RemoveListener(SetHealth);

        SkillController.OnPassiveSkillUse.RemoveListener(() => canTakeDamage = false);
        SkillController.OnPassiveSkillEnd.RemoveListener(() => canTakeDamage = true);

    }
    public void Die()
    {
        GameManager.Instance.CompeleteStage(false);
    }
}
