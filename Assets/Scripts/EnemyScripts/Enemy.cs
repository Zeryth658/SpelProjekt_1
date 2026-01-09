using System;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
   
    private float currentHealth{get; set;}
    public SpriteRenderer SpriteRenderer {get; set;}
    public bool IsFacingRight { get; set; } = true;
    public bool Spotted { get; set; }
    
    public EnemyStateMachine StateMachine { get; set; } 
    public EnemyIdleState  IdleState { get; set; } 
    public EnemyAttackRecoveryState AttackRecoveryState { get; set; } 
    public EnemyAttackState AttackState { get; set; } 
    public EnemySpottedPlayerState  SpottedState { get; set; } 
    public EnemyPreparingShotState  PreparingShotState { get; set; }
    public EnemyChaseState ChaseState { get; set; }
    
    
    [Header("Settings")]
    public float MaxHealth  = 5f;
    public float detectionRange = 15f;
    public float spottingDelay = 0.7f;
    public float preparingShotTime = 0.4f;
    public float recoveryTime = 0.5f;
    public float shootingRange = 7f;
    [SerializeField] float hitStopDuration = 0.05f;
    [SerializeField] float hitStopTimeScale = 0f;
    public AudioSource audioSource;
    public AudioClip deathSound;
    
    [Header("References")]
    public Hurtbox hurtbox;
    public GameObject blood;
    [SerializeField] private GameObject healthUp;
    public EnemyShooter Shooter { get; set; }
    public OrbitingWeapon WeaponRotation { get; set; }
    
    public EnemyMovement Movement { get; set; }
    
    public PathMovement PathMovement { get; set; }
    
    public Patrol Patrol { get; set; }
    
    public Animator Animator { get; set; }

    private void Awake()
    {
        Movement = GetComponent<EnemyMovement>();
        WeaponRotation = GetComponentInChildren<OrbitingWeapon>();
        PathMovement = GetComponent<PathMovement>();
        Patrol = GetComponent<Patrol>();
        Shooter = GetComponent<EnemyShooter>();
        Animator = GetComponent<Animator>();
        StateMachine = new EnemyStateMachine();
        ChaseState = new EnemyChaseState(this, StateMachine);
        PreparingShotState = new EnemyPreparingShotState(this, StateMachine);
        IdleState = new EnemyIdleState(this, StateMachine);   
        AttackRecoveryState = new EnemyAttackRecoveryState(this, StateMachine);
        AttackState = new EnemyAttackState(this, StateMachine);
        SpottedState = new EnemySpottedPlayerState(this, StateMachine);
    }

    public void Start()
    {
        currentHealth = MaxHealth;
        
        SpriteRenderer = GetComponent<SpriteRenderer>();
        PathMovement.Initialize(GridManager.Instance.obstacleMask);
        StateMachine.Initialize(IdleState);
    }

    public void Update()
    {
        WeaponRotation.UpdateAimRotation();
        StateMachine.CurrentState.FrameUpdate();
    }

    public void FixedUpdate()
    {
        StateMachine.CurrentState.PhysicsUpdate();
    }
    
    
    public bool PlayerDetected()
    {
        if (!Shooter.target) return false;
        if (Vector3.Distance(transform.position, Shooter.target.position) <= detectionRange)
        {
            return Shooter.TargetInLineOfSight();
        }

        return false;
    }

    public bool RangeCheck()
    {
        float distance = Vector2.Distance(transform.position, Shooter.target.position);
        if (distance > shootingRange)
        {
            return true;
        }
        return false;
    }

    public void AimChecker()
    {
        if (PlayerDetected())
        {
            WeaponRotation.isAiming = true;
        }
        else
        {
            WeaponRotation.isAiming = false;
        }
    }


    public void TakeDamage(float amount, GameObject source)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
            Die();
    }

    void Die()
    {
        PoolManager.Spawn(blood, transform.position, Quaternion.identity);
        PoolManager.Spawn(healthUp, transform.position, Quaternion.identity);
        //AudioSource.PlayClipAtPoint(deathSound, transform.position);
        SoundManager.PlaySound(SoundType.EnemyDeath);
        HitstopManager.Instance.DoHitstop(hitStopDuration, hitStopTimeScale);
        PoolManager.Despawn(gameObject);
    }

    private void AnimationTriggerEvent(AnimationTriggerType triggerType)
    {
        StateMachine.CurrentState.AnimationTriggerEvent(triggerType);
    }
    
    public enum AnimationTriggerType 
    {
        EnemyDamaged,
    }
}
