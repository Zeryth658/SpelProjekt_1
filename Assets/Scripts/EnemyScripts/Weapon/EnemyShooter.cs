using System;
using UnityEngine;
using System.Collections;
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
    public AudioClip shootSound;
    
    [Header("Shoot behaviour")]
	public int shotAmount = 1;
	public float shotDelay = 0.2f; 
    public ShotPattern shotpattern;
    public AimBehaviour aimBehaviour;
    public LayerMask obstacleMask;

   
    private Vector2 direction;
    private float time;
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
        
        if (target == null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
                target = player.transform;
            else
                Debug.LogError("Player not found");
        }
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

        
        
        
        StartCoroutine(MultiShoot());
        
        
    }

    private IEnumerator MultiShoot()
    {
        for (int i = 0; i < shotAmount; i++)
        {
            direction = aimBehaviour.SetTarget(firePoint, target);
            //AudioSource.PlayClipAtPoint(shootSound, transform.position);
            SoundManager.PlaySound(SoundType.EnemyAttack);
            shotpattern.Shoot(this, firePoint, direction);
            yield return new WaitForSeconds(shotDelay);
        }
        yield return null;
    }

    public void SpawnBullet(Vector3 spawnPosition, Vector3 spawnDirection)
    {
        float angle = Mathf.Atan2(spawnDirection.y, spawnDirection.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        GameObject bulletObj = PoolManager.Spawn(bulletPrefab, spawnPosition, rotation);
        Bullet bullet = bulletObj.GetComponent<Bullet>();
        bullet.Initialize(spawnDirection, bulletDamage, this.gameObject, bulletSpeed, bulletLifeTime);
    }
    

}
