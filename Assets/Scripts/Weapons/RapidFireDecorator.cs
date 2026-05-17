// Decorator: multiplies the inner weapon's fire rate.
// Does not modify Fire(). Only changes how often PlayerShooter calls it.
public class RapidFireDecorator : WeaponDecorator
{
    private readonly float multiplier;

    public RapidFireDecorator(IWeapon weapon, float multiplier = 2f) : base(weapon)
    {
        this.multiplier = multiplier;
    }

    public override float FireRate => inner.FireRate * multiplier;
}
