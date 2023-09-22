using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterHealthController : MonoBehaviour, IDamagable
{
    private float currentHealth;
    bool canTakeDamage;

    public void SetHealth(float health)
    {
        currentHealth = health;
        canTakeDamage = true;
    }
    public int GetCurrentHealth()
    {
        return 0;
    }

    public void TakeDamage(float damage)
    {
        if (!canTakeDamage)
            return;
        if (currentHealth - damage <= 0)
            Die();
        else
            currentHealth -= damage;

    }
    private void OnEnable()
    {
        SkillController.OnPassiveSkillUse.AddListener(() => canTakeDamage = false);
        SkillController.OnPassiveSkillEnd.AddListener(() => canTakeDamage = true);
    }
    private void OnDisable()
    {
        SkillController.OnPassiveSkillUse.RemoveListener(() => canTakeDamage = false);
        SkillController.OnPassiveSkillEnd.RemoveListener(() => canTakeDamage = true);

    }
    public void Die()
    {
        GameManager.Instance.CompeleteStage(false);
    }

    public void Regenerate(float regenAmount)
    {

    }

}
