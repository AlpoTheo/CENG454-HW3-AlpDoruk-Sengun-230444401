using UnityEngine;

// Wires all systems together at Start. Acts as the composition root —
// creates the Weapon decorator chain and initializes WaveManager.
public class GameSetup : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private ObjectPool    projectilePool;
    [SerializeField] private ObjectPool    enemyPool;
    [SerializeField] private Transform     coreTransform;
    [SerializeField] private PlayerShooter playerShooter;
    [SerializeField] private Transform     firePoint;
    [SerializeField] private WaveManager   waveManager;

    private void Start()
    {
        // Decorator chain: Base → RapidFire (x2 speed) → DoubleShot
        IWeapon base_weapon    = new BaseWeapon(projectilePool, firePoint, 2f);
        IWeapon rapidWeapon    = new RapidFireDecorator(base_weapon, 2f);
        IWeapon doubleShotWeapon = new DoubleShotDecorator(rapidWeapon);

        playerShooter.SetWeapon(doubleShotWeapon);
        waveManager.Init(enemyPool, coreTransform);
    }
}
