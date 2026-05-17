using UnityEngine;

// Strategy 1: Enemy moves in a straight line toward the core.
// Used in Wave 1. Simple and predictable.
public class DirectMoveStrategy : IMovementStrategy
{
    public void Move(Transform self, Transform target, float speed)
    {
        Vector3 direction = (target.position - self.position).normalized;
        self.position += direction * speed * Time.deltaTime;
    }
}
