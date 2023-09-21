using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterHealthController : MonoBehaviour, IDamagable
{
    private float currentHealth;
    public void SetHealth(float health)
    {
        currentHealth = health;
    }
    public int GetCurrentHealth()
    {
        return 0;
    }

    public void TakeDamage(float damage)
    {

    }

    public void Die()
    {

    }

    public void Regenerate(float regenAmount)
    {

    }

}
