using UnityEngine;
using System;

// Drives waves with Update + timer instead of a coroutine.
// Strategy is reassigned per wave so EnemyController stays unchanged.
public class WaveManager : MonoBehaviour
{
    [SerializeField] private int   totalWaves       = 3;
    [SerializeField] private int   baseEnemyCount   = 5;
    [SerializeField] private float betweenWaveDelay = 4f;
    [SerializeField] private float startDelay       = 2f;
    [SerializeField] private float spawnRadius      = 8f;

    private enum Phase { WaitingToStart, WaveActive, BetweenWaves, Finished }

    private ObjectPool enemyPool;
    private Transform  coreTransform;
    private Phase      phase = Phase.WaitingToStart;
    private int        currentWave;
    private int        enemiesAlive;
    private float      timer;
    private bool       initialized;

    public static event Action<int> OnWaveStarted;
    public static event Action       OnAllWavesCleared;

    public void Init(ObjectPool pool, Transform core)
    {
        Debug.Log("[WaveManager] Init called");
        enemyPool     = pool;
        coreTransform = core;
        EnemyController.OnEnemyDied += HandleEnemyDied;
        timer       = startDelay;
        phase       = Phase.WaitingToStart;
        initialized = true;
    }

    private void OnDestroy()
    {
        EnemyController.OnEnemyDied -= HandleEnemyDied;
    }

    private void Update()
    {
        if (!initialized) return;

        switch (phase)
        {
            case Phase.WaitingToStart:
                timer -= Time.deltaTime;
                if (timer <= 0f) StartNextWave();
                break;

            case Phase.WaveActive:
                if (enemiesAlive <= 0)
                {
                    if (currentWave >= totalWaves)
                    {
                        Debug.Log("[WaveManager] All waves cleared");
                        phase = Phase.Finished;
                        OnAllWavesCleared?.Invoke();
                    }
                    else
                    {
                        phase = Phase.BetweenWaves;
                        timer = betweenWaveDelay;
                    }
                }
                break;

            case Phase.BetweenWaves:
                timer -= Time.deltaTime;
                if (timer <= 0f) StartNextWave();
                break;
        }
    }

    private void StartNextWave()
    {
        currentWave++;
        enemiesAlive = baseEnemyCount + (currentWave - 1) * 2;
        Debug.Log($"[WaveManager] Starting wave {currentWave} with {enemiesAlive} enemies");
        OnWaveStarted?.Invoke(currentWave);

        for (int i = 0; i < enemiesAlive; i++) SpawnEnemy();

        phase = Phase.WaveActive;
    }

    private void SpawnEnemy()
    {
        float   angle    = UnityEngine.Random.Range(0f, 360f) * Mathf.Deg2Rad;
        Vector2 spawnPos = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * spawnRadius;

        GameObject obj = enemyPool.Get();
        obj.transform.position = spawnPos;

        IMovementStrategy strategy = currentWave >= 2
            ? (IMovementStrategy)new ZigZagMoveStrategy()
            : new DirectMoveStrategy();

        obj.GetComponent<EnemyController>()?.Init(strategy, coreTransform, enemyPool);
        Debug.Log($"[WaveManager] Spawned enemy at {spawnPos}");
    }

    private void HandleEnemyDied(EnemyController _)
    {
        enemiesAlive = Mathf.Max(0, enemiesAlive - 1);
    }
}
