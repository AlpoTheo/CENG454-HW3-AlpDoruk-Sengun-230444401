using UnityEngine;

// Abstract base for all weapon decorators.
// Wraps any IWeapon and forwards calls to it by default —
// concrete decorators only override what they change.
public abstract class WeaponDecorator : IWeapon
{
    protected readonly IWeapon inner;

    protected WeaponDecorator(IWeapon weapon)
    {
        inner = weapon;
    }

    public virtual float FireRate => inner.FireRate;

    public virtual void Fire(Vector2 direction) => inner.Fire(direction);
}
