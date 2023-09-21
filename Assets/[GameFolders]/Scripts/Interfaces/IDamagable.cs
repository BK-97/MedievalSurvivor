public interface IDamagable
{
    void SetHealth();
    int GetCurrentHealth();
    void TakeDamage(int damage);
    void Die();
    void Regenerate(int regenAmount);
}
