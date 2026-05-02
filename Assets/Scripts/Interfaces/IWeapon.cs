using UnityEngine;

// Weapon contract used by PlayerShooter.
// Decorator pattern wraps this interface to add fire-rate or multi-shot behaviour.
public interface IWeapon
{
    float FireRate { get; }
    void Fire(Vector2 direction);
}
