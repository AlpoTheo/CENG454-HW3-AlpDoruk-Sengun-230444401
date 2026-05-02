using UnityEngine;

// Strategy pattern contract for enemy movement.
// EnemyController depends on this interface, not on concrete move classes.
public interface IMovementStrategy
{
    void Move(Transform self, Transform target, float speed);
}
