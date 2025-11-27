using UnityEngine;

public class AimAtPlayer : AimBehaviour
{
    public override Vector2 SetTarget(Transform enemy, Transform target)
    {
        return (target.position - enemy.position).normalized;
    }
}
