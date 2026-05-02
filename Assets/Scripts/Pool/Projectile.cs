using UnityEngine;

// Pooled projectile. Implements IPoolable so ObjectPool can reset state on reuse.
// On trigger, damages IDamageable enemies and returns itself to the pool.
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class Projectile : MonoBehaviour, IPoolable
{
    [SerializeField] private float speed    = 12f;
    [SerializeField] private float damage   = 10f;
    [SerializeField] private float lifetime = 3f;

    private Vector2      moveDirection;
    private ObjectPool   ownerPool;
    private float        timer;
    private Rigidbody2D  rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Init(Vector2 direction, ObjectPool pool)
    {
        moveDirection = direction.normalized;
        ownerPool     = pool;
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= lifetime)
            ReturnToPool();
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = moveDirection * speed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Enemy")) return;

        IDamageable target = other.GetComponent<IDamageable>();
        target?.TakeDamage(damage);
        ReturnToPool();
    }

    private void ReturnToPool()
    {
        ownerPool?.ReturnToPool(gameObject);
    }

    // --- IPoolable ---
    public void OnSpawn()
    {
        timer = 0f;
    }

    public void OnDespawn()
    {
        rb.linearVelocity = Vector2.zero;
    }
}
