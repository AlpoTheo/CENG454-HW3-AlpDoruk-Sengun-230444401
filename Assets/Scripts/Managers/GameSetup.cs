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
        Debug.Log("[GameSetup] Start invoked");

        if (projectilePool == null) { Debug.LogError("[GameSetup] projectilePool is NULL"); return; }
        if (enemyPool      == null) { Debug.LogError("[GameSetup] enemyPool is NULL");      return; }
        if (coreTransform  == null) { Debug.LogError("[GameSetup] coreTransform is NULL");  return; }
        if (playerShooter  == null) { Debug.LogError("[GameSetup] playerShooter is NULL");  return; }
        if (firePoint      == null) { Debug.LogError("[GameSetup] firePoint is NULL");      return; }
        if (waveManager    == null) { Debug.LogError("[GameSetup] waveManager is NULL");    return; }

        IWeapon basicWeapon    = new BaseWeapon(projectilePool, firePoint, 2f);
        IWeapon rapidWeapon    = new RapidFireDecorator(basicWeapon, 2f);
        IWeapon decoratedWeapon = new DoubleShotDecorator(rapidWeapon);

        playerShooter.SetWeapon(decoratedWeapon);
        waveManager.Init(enemyPool, coreTransform);

        Debug.Log("[GameSetup] Weapon set and WaveManager initialized");
    }
}
