using UnityEngine;

// Concrete base weapon. Spawns one projectile per Fire() call.
// Fire rate is controlled externally by PlayerShooter to keep this class simple.
public class BaseWeapon : IWeapon
{
    private readonly ObjectPool    projectilePool;
    private readonly Transform     firePoint;
    private readonly float         fireRate;

    public float FireRate => fireRate;

    public BaseWeapon(ObjectPool pool, Transform fp, float rate = 2f)
    {
        projectilePool = pool;
        firePoint      = fp;
        fireRate       = rate;
    }

    public void Fire(Vector2 direction)
    {
        GameObject obj = projectilePool.Get();
        obj.transform.position = firePoint.position;
        obj.transform.rotation = Quaternion.identity;
        obj.GetComponent<Projectile>()?.Init(direction, projectilePool);
    }
}
