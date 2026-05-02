// Any game object that can receive damage implements this.
// Decouples damage logic from concrete types (Core, Enemy, etc.)
public interface IDamageable
{
    float MaxHealth { get; }
    float CurrentHealth { get; }
    bool IsAlive { get; }
    void TakeDamage(float amount);
}
