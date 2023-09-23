using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    private Slider healthBar;
    private void Start()
    {
        healthBar = GetComponent<Slider>();
    }
    private void OnEnable()
    {
        CharacterHealthController.OnHealthSet.AddListener(SetBar);
        CharacterHealthController.OnHealthChange.AddListener(ChangeHealth);
    }
    private void OnDisable()
    {
        CharacterHealthController.OnHealthSet.RemoveListener(SetBar);
        CharacterHealthController.OnHealthChange.RemoveListener(ChangeHealth);
    }
    private void SetBar(float maxHealth)
    {
        healthBar.maxValue = maxHealth;
        healthBar.value = maxHealth;
    }
    private void ChangeHealth(float currentHealth)
    {
        healthBar.value = currentHealth;
    }
}
