using UnityEngine;

public class OrbitingWeapon : MonoBehaviour
{
    private Transform weapon;          
    private Transform enemy;            
    private Transform target;           
    private AimBehaviour aimBehaviour;
    private EnemyShooter shooter;
    public float AimSpeed = 720f;
    private SpriteRenderer spriteRenderer;
    public bool isAiming { get; set; }

    void Awake()
    {
        weapon = transform;
        shooter = GetComponentInParent<EnemyShooter>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (shooter == null)
        {
            Debug.LogError("WeaponOrbit: No EnemyShooter found in parent!");
            return;
        }
        enemy = shooter.transform;
        target = shooter.target;
        aimBehaviour = shooter.aimBehaviour;
    }


    public void UpdateAimRotation()
    {
        if (!isAiming) return;
        Vector2 dirToTarget = aimBehaviour.SetTarget(enemy, target);
        float targetAngle = Mathf.Atan2(dirToTarget.y, dirToTarget.x) * Mathf.Rad2Deg;
            
        Vector2 dirToWeapon = transform.position - enemy.position;
        float currentAngle = Mathf.Atan2(dirToWeapon.y, dirToWeapon.x) * Mathf.Rad2Deg;

            
        float newAngle = Mathf.MoveTowardsAngle(
            currentAngle,
            targetAngle, 
            AimSpeed * Time.deltaTime
        );
            
        float deltaAngle = Mathf.DeltaAngle(currentAngle, newAngle);
        weapon.transform.RotateAround(enemy.position, Vector3.forward, deltaAngle);
        
        float rotation = transform.eulerAngles.z;
        
        if (rotation > 180) rotation -= 360;

   
        if (rotation > 90 || rotation < -90)
        {
            spriteRenderer.flipY = true;
        }
        else spriteRenderer.flipY = false;
        
    }
    

    
    
}
