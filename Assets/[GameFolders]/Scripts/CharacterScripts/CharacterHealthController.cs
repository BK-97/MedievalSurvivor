using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CharacterHealthController : MonoBehaviour, IDamagable
{

    private float currentHealth;
    private float _maxHealth;
    bool canTakeDamage;
    public static FloatEvent OnHealthChange = new FloatEvent();
    public static FloatEvent OnHealthSet = new FloatEvent();
    public static UnityEvent OnCharacterDie = new UnityEvent();

    public void SetHealth(float maxHealth)
    {
        if (PlayerPrefs.GetFloat(PlayerPrefKeys.CurrentHealth, maxHealth) == 0)
            PlayerPrefs.SetFloat(PlayerPrefKeys.CurrentHealth, maxHealth);

        currentHealth = PlayerPrefs.GetFloat(PlayerPrefKeys.CurrentHealth, maxHealth);
        OnHealthChange.Invoke(currentHealth);
        _maxHealth = maxHealth;
        canTakeDamage = true;
    }

    public void TakeDamage(float damage)
    {
        if (!canTakeDamage)
            return;
        if (currentHealth - damage <= 0)
            Die();
        else
        {

            currentHealth -= damage;
            PlayerPrefs.SetFloat(PlayerPrefKeys.CurrentHealth, currentHealth);
            OnHealthChange.Invoke(currentHealth);
        }
    }
    private void OnEnable()
    {
        CharacterHealthController.OnHealthSet.AddListener(SetHealth);
        
        SkillController.OnPassiveSkillUse.AddListener(() => canTakeDamage = false);
        SkillController.OnPassiveSkillEnd.AddListener(() => canTakeDamage = true);
    }
    private void OnDisable()
    {
        CharacterHealthController.OnHealthSet.RemoveListener(SetHealth);

        SkillController.OnPassiveSkillUse.RemoveListener(() => canTakeDamage = false);
        SkillController.OnPassiveSkillEnd.RemoveListener(() => canTakeDamage = true);
    }
    public void Die()
    {
        OnCharacterDie.Invoke();
        GameManager.Instance.CompeleteStage(false);
        PlayerPrefs.SetFloat(PlayerPrefKeys.CurrentHealth, _maxHealth);
    }
}
