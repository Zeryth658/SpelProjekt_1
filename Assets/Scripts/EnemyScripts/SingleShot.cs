using UnityEngine;

[CreateAssetMenu(menuName = "Shooting/SingleShot")]
public class SingleShot : ShotPattern
{
    public override void Shoot(EnemyShooter shooter, Transform firePoint, Vector2 aimDirection)
    { 
        shooter.SpawnBullet();
    }
} 
