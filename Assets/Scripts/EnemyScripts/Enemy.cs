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
    
    [Header("Settings")]
    public float MaxHealth  = 5f;
    public float detectionRange = 10f;
    public float spottingDelay = 0.7f;
    public float preparingShotTime = 0.4f;
    public float recoveryTime = 0.5f;
    
    [Header("References")]
    public Hurtbox hurtbox;
    
    public EnemyShooter Shooter { get; set; }
    public OrbitingWeapon WeaponRotation { get; set; }

    private void Awake()
    {
        WeaponRotation = GetComponentInChildren<OrbitingWeapon>();
        Shooter = GetComponent<EnemyShooter>();
        StateMachine = new EnemyStateMachine();
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

        return Vector3.Distance(transform.position, Shooter.target.position) <= detectionRange &&
                Shooter.TargetInLineOfSight();


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
        Debug.Log($"{gameObject.name} took {amount} from {source}");
        if (currentHealth <= 0)
            Die();
    }
    
    void Die()
    {
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
