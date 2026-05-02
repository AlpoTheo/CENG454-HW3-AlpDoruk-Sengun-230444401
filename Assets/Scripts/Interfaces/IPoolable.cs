// Object Pool contract. OnSpawn resets state when retrieved from pool;
// OnDespawn cleans up (e.g. unsubscribes events) before returning.
public interface IPoolable
{
    void OnSpawn();
    void OnDespawn();
}
