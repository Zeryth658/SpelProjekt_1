using UnityEngine;

public class EnemyShooter : MonoBehaviour
{
    [Header("Bullet stats")]
    public float bulletSpeed;
    public float bulletLifeTime;
    public float bulletDamage;
    public float attackCooldown = 1f;
    
    [Header("References")]
    public Transform firePoint;
    public Transform target;
    public GameObject bulletPrefab;
    
    [Header("Shoot behaviour")]
    public ShotPattern shotpattern;
    public AimBehaviour aimBehaviour;

    private float cooldownTimer;
    private Vector2 direction;
    
    // Update is called once per frame
    void Update()
    {
        //cooldownTimer -= Time.deltaTime;

        if (cooldownTimer <= 0)
        {
            cooldownTimer = attackCooldown;
            Shoot();
        }
        else
        {
            cooldownTimer -= Time.deltaTime;
        }
        
    }

    private void Shoot()
    {
        if (aimBehaviour == null || target == null || shotpattern == null)
        {
            Debug.Log($"{gameObject.name} I can't shoot something missing. AimBehavior =  {aimBehaviour}, target =  {target}, shotpattern =  {shotpattern}");
            return; 
        }

        direction = aimBehaviour.SetTarget(firePoint, target);
        
        shotpattern.Shoot(this, firePoint, direction);
    }

    public void SpawnBullet(Vector3 spawnPosition, Vector3 spawnDirection)
    {
        float angle = Mathf.Atan2(spawnDirection.y, spawnDirection.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        GameObject bulletObj = PoolManager.Spawn(bulletPrefab, spawnPosition, rotation);
        Bullet bullet = bulletObj.GetComponent<Bullet>();
        bullet.Initialize(direction, bulletDamage, this.gameObject, bulletSpeed, bulletLifeTime);
    }
}
