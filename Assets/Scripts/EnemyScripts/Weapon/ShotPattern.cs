using UnityEngine;

public abstract class ShotPattern : ScriptableObject
{ 
    public abstract void Shoot(EnemyShooter shooter, Transform firePoint, Vector2 aimDirection);
}
