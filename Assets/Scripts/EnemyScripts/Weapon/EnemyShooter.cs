using System;
using UnityEngine;

public class EnemyShooter : MonoBehaviour
{
    [Header("Bullet stats")]
    public float bulletSpeed;
    public float bulletLifeTime;
    public float bulletDamage;
    
    [Header("References")]
    public Transform firePoint;
    public Transform target;
    public GameObject bulletPrefab;
    
    [Header("Shoot behaviour")]
    public ShotPattern shotpattern;
    public AimBehaviour aimBehaviour;
    public LayerMask obstacleMask;

   
    private Vector2 direction;
    
    private float bulletRadius;
    // Update is called once per frame
    
    private void Start()
    {
        if (aimBehaviour == null || target == null || shotpattern == null)
        {
            Debug.Log($"{gameObject.name} I can't shoot something missing. AimBehavior =  {aimBehaviour}, target =  {target}, shotpattern =  {shotpattern}");
            return; 
        }
    }

    private void Awake()
    {
        var collider = bulletPrefab.GetComponent<CircleCollider2D>();
        bulletRadius = collider.radius + 0.1f;
    }
    

    public bool CanShoot
    {
        get
        {
            return TargetInLineOfSight();
        }
    }
    




    public bool TargetInLineOfSight()
    {
        
        if (target == null) return false;
        
      Vector2 dir = (target.position - transform.position).normalized;
      float distance = Vector2.Distance(transform.position, target.position);
      RaycastHit2D hit = Physics2D.CircleCast(transform.position, bulletRadius, dir, distance, obstacleMask);
      if (hit.collider != null) return false;

      
      return true;
    }

    public void Shoot()
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
