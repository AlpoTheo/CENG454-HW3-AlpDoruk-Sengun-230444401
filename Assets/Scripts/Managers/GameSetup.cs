using UnityEngine;

// Composition root: creates the Weapon decorator chain at runtime
// and hands a fully wrapped IWeapon to PlayerShooter, then boots the WaveManager.
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
        if (projectilePool == null || enemyPool == null || coreTransform == null
            || playerShooter == null || firePoint == null || waveManager == null)
        {
            Debug.LogError("[GameSetup] One or more references are missing in the Inspector.");
            return;
        }

        IWeapon basicWeapon     = new BaseWeapon(projectilePool, firePoint, 2f);
        IWeapon rapidWeapon     = new RapidFireDecorator(basicWeapon, 2f);
        IWeapon decoratedWeapon = new DoubleShotDecorator(rapidWeapon);

        playerShooter.SetWeapon(decoratedWeapon);
        waveManager.Init(enemyPool, coreTransform);
    }
}
