using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    public Slider healthBar;
    public GameObject chains;
    private void Start()
    {
        ChainSet(false);
    }
    private void OnEnable()
    {
        CharacterHealthController.OnHealthSet.AddListener(SetBar);
        SkillController.OnPassiveSkillUse.AddListener(()=>ChainSet(true));
        SkillController.OnPassiveSkillEnd.AddListener(()=>ChainSet(false));
        CharacterHealthController.OnHealthChange.AddListener(ChangeHealth);
    }
    private void OnDisable()
    {
        CharacterHealthController.OnHealthSet.RemoveListener(SetBar);
        SkillController.OnPassiveSkillUse.RemoveListener(() => ChainSet(true));
        SkillController.OnPassiveSkillEnd.RemoveListener(() => ChainSet(false));
        CharacterHealthController.OnHealthChange.RemoveListener(ChangeHealth);
    }
    private void ChainSet(bool status)
    {
        chains.SetActive(status);
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
