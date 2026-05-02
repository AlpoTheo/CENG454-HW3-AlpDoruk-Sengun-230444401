using UnityEngine;

// Strategy 3: Enemy spirals inward toward the core instead of approaching directly.
// Demonstrates that adding a new movement style requires zero changes to EnemyController
// or to existing strategy implementations.
public class OrbitMoveStrategy : IMovementStrategy
{
    private readonly float orbitFactor;

    public OrbitMoveStrategy(float orbitFactor = 0.6f)
    {
        this.orbitFactor = orbitFactor;
    }

    public void Move(Transform self, Transform target, float speed)
    {
        Vector3 toTarget = target.position - self.position;
        Vector3 inward   = toTarget.normalized;
        Vector3 tangent  = new Vector3(-inward.y, inward.x, 0f);

        Vector3 combined = inward * (1f - orbitFactor) + tangent * orbitFactor;
        self.position   += combined.normalized * speed * Time.deltaTime;
    }
}
