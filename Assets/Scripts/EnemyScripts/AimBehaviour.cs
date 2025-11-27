using UnityEngine;

public abstract class AimBehaviour : ScriptableObject
{
    public abstract Vector2 SetTarget(Transform enemy, Transform target);
}
