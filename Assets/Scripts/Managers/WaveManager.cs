using UnityEngine;
using System;
using System.Collections;

// Spawns enemy waves from a pool. Assigns movement strategy based on wave number
// to demonstrate runtime Strategy swapping without modifying EnemyController.
public class WaveManager : MonoBehaviour
{
    [SerializeField] private int   totalWaves       = 3;
    [SerializeField] private int   baseEnemyCount   = 5;
    [SerializeField] private float betweenWaveDelay = 4f;
    [SerializeField] private float spawnRadius      = 9f;

    private ObjectPool enemyPool;
    private Transform  coreTransform;
    private int        currentWave;
    private int        enemiesAlive;

    // --- Observer events ---
    public static event Action<int> OnWaveStarted;
    public static event Action       OnAllWavesCleared;

    public void Init(ObjectPool pool, Transform core)
    {
        enemyPool     = pool;
        coreTransform = core;
        EnemyController.OnEnemyDied += HandleEnemyDied;
        StartCoroutine(RunWaves());
    }

    private void OnDestroy()
    {
        EnemyController.OnEnemyDied -= HandleEnemyDied;
    }

    private IEnumerator RunWaves()
    {
        yield return new WaitForSeconds(1.5f);

        for (int w = 1; w <= totalWaves; w++)
        {
            currentWave  = w;
            enemiesAlive = baseEnemyCount + (w - 1) * 2;

            OnWaveStarted?.Invoke(currentWave);
            SpawnWave(enemiesAlive);

            yield return new WaitUntil(() => enemiesAlive <= 0);

            if (w < totalWaves)
                yield return new WaitForSeconds(betweenWaveDelay);
        }

        OnAllWavesCleared?.Invoke();
    }

    private void SpawnWave(int count)
    {
        for (int i = 0; i < count; i++)
            SpawnEnemy();
    }

    private void SpawnEnemy()
    {
        Vector2 spawnPos = UnityEngine.Random.insideUnitCircle.normalized * spawnRadius;

        GameObject obj = enemyPool.Get();
        obj.transform.position = spawnPos;

        // Strategy switches per wave — no change needed inside EnemyController.
        IMovementStrategy strategy = currentWave >= 2
            ? (IMovementStrategy)new ZigZagMoveStrategy()
            : new DirectMoveStrategy();

        obj.GetComponent<EnemyController>()?.Init(strategy, coreTransform, enemyPool);
    }

    private void HandleEnemyDied(EnemyController _)
    {
        enemiesAlive = Mathf.Max(0, enemiesAlive - 1);
    }
}
