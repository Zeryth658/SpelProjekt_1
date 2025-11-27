using UnityEngine;

public interface IShotPattern 
{ 
    void Fire(EnemyShooter shooter, Transform firePoint, Transform player);
}
