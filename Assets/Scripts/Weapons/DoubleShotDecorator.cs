using UnityEngine;

// Decorator: fires two projectiles at a slight spread angle per Fire() call.
// Stacks on top of any IWeapon without modifying it.
public class DoubleShotDecorator : WeaponDecorator
{
    private readonly float spreadDegrees;

    public DoubleShotDecorator(IWeapon weapon, float spread = 12f) : base(weapon)
    {
        spreadDegrees = spread;
    }

    public override void Fire(Vector2 direction)
    {
        inner.Fire(Rotate(direction,  spreadDegrees));
        inner.Fire(Rotate(direction, -spreadDegrees));
    }

    private static Vector2 Rotate(Vector2 v, float degrees)
    {
        float rad = degrees * Mathf.Deg2Rad;
        return new Vector2(
            v.x * Mathf.Cos(rad) - v.y * Mathf.Sin(rad),
            v.x * Mathf.Sin(rad) + v.y * Mathf.Cos(rad)
        );
    }
}
