using UnityEngine;

[CreateAssetMenu(menuName = "Shooting/SpreadMultiShot")]

public class SpreadMultiShot : ShotPattern
{
    
    public float spreadAngle = 10f;
    public int bulletAmount = 3;
    
    [Header("Randomness Settings (OVERRIDES SPREAD ANGLE)")]
    public bool randomAngle = false;
    public float minSpreadAngle = 5f;
    public float maxSpreadAngle = 15f;
    public override void Shoot(EnemyShooter shooter, Transform firePoint, Vector2 aimDirection)
    { 
        aimDirection = aimDirection.normalized;
   
        
        float startAngle = -spreadAngle * 0.5f;
        float angleRoration = spreadAngle / (bulletAmount - 1);

        for (int i = 0; i < bulletAmount; i++)
        {
            if (randomAngle)
            {
                spreadAngle = Random.Range(minSpreadAngle, maxSpreadAngle);
                startAngle = -spreadAngle * 0.5f;
                angleRoration = spreadAngle / (bulletAmount - 1);
            }
            float angle = startAngle + angleRoration * i;
            Vector2 direction = Rotate(aimDirection, angle);

            shooter.SpawnBullet(firePoint.position, direction);
        }
    }
    
    private Vector2 Rotate(Vector2 v, float degrees)
    {
        float rad = degrees * Mathf.Deg2Rad;
        float sin = Mathf.Sin(rad);
        float cos = Mathf.Cos(rad);

        return new Vector2(
            v.x * cos - v.y * sin,
            v.x * sin + v.y * cos
        );
    }
}
