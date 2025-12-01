using System;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    [field: SerializeField] public float MaxHealth { get; set; } = 5f;
    public float currentHealth{get; set;}
    public Rigidbody2D rb {get; set;}
    public bool IsFacingRight { get; set; } = true;
    public Hurtbox hurtbox;

    public void Start()
    {
        currentHealth = MaxHealth;
        rb = GetComponent<Rigidbody2D>();
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
        // TODO
    }
    
    public enum AnimationTriggerType 
    {
        EnemyDamaged,
    }
}
