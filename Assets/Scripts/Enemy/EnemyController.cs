using UnityEngine;
using System;

// Enemy that implements IDamageable and IPoolable.
// Movement is fully delegated to IMovementStrategy. Swappable per wave.
// Fires OnEnemyDied so WaveManager can count kills without tight coupling.
public class EnemyController : MonoBehaviour, IDamageable, IPoolable
{
    [SerializeField] private float maxHealth   = 30f;
    [SerializeField] private float speed       = 2f;
    [SerializeField] private float attackDmg   = 10f;
    [SerializeField] private float attackRate  = 1f;
    [SerializeField] private float attackRange = 1.2f;

    private float              currentHealth;
    private IMovementStrategy  moveStrategy;
    private Transform          coreTransform;
    private ObjectPool         ownerPool;
    private float              attackTimer;

    public float MaxHealth     => maxHealth;
    public float CurrentHealth => currentHealth;
    public bool  IsAlive       => currentHealth > 0f;

    public static event Action<EnemyController> OnEnemyDied;

    // Called by WaveManager each time this enemy is spawned from the pool.
    public void Init(IMovementStrategy strategy, Transform core, ObjectPool pool)
    {
        moveStrategy  = strategy;
        coreTransform = core;
        ownerPool     = pool;
        currentHealth = maxHealth;
        attackTimer   = 0f;
    }

    private void Update()
    {
        if (!IsAlive || coreTransform == null) return;

        float dist = Vector2.Distance(transform.position, coreTransform.position);

        if (dist > attackRange)
        {
            moveStrategy.Move(transform, coreTransform, speed);
        }
        else
        {
            attackTimer += Time.deltaTime;
            if (attackTimer >= 1f / attackRate)
            {
                attackTimer = 0f;
                coreTransform.GetComponent<IDamageable>()?.TakeDamage(attackDmg);
            }
        }
    }

    public void TakeDamage(float amount)
    {
        if (!IsAlive) return;
        currentHealth -= amount;
        if (currentHealth <= 0f)
            Die();
    }

    private void Die()
    {
        OnEnemyDied?.Invoke(this);
        ownerPool?.ReturnToPool(gameObject);
    }

    // --- IPoolable ---
    public void OnSpawn()
    {
        currentHealth = maxHealth;
        attackTimer   = 0f;
    }

    // OnDespawn: no external subscriptions to unsubscribe here,
    // so ghost-subscriber risk does not apply to this class.
    public void OnDespawn() { }
}
