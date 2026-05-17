using UnityEngine;

// Strategy 2: Enemy weaves side-to-side while approaching the core.
// Used in Wave 2+. Harder for the player to hit.
// Swappable into EnemyController without changing EnemyController's code.
public class ZigZagMoveStrategy : IMovementStrategy
{
    private readonly float amplitude;
    private readonly float frequency;
    private float time;

    public ZigZagMoveStrategy(float amplitude = 2f, float frequency = 2f)
    {
        this.amplitude = amplitude;
        this.frequency = frequency;
    }

    public void Move(Transform self, Transform target, float speed)
    {
        time += Time.deltaTime;

        Vector3 toTarget     = (target.position - self.position).normalized;
        Vector3 perpendicular = new Vector3(-toTarget.y, toTarget.x, 0f);
        Vector3 combined      = toTarget + perpendicular * Mathf.Sin(time * frequency) * amplitude;

        self.position += combined.normalized * speed * Time.deltaTime;
    }
}
