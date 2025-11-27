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


    // Update is called once per frame
    void Update()
    {
        
    }
}
