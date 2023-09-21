public interface IDamagable
{
    void SetHealth(float health);
    int GetCurrentHealth();
    void TakeDamage(float damage);
    void Die();
    void Regenerate(float regenAmount);
}
