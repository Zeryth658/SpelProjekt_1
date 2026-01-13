using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    //public float maxHealth = 15f;
    //public float currentHealth = 5f;

    public HealthData health;
    public HealthBar healthBar;
    public Hurtbox hurtbox;
    private Animator animator;

    public bool immune = false;
    public bool isDead = false;

    public void Awake()
    {
        isDead = false;
        animator = GetComponentInChildren<Animator>();
    }

    public void Start()
    {
        healthBar.SetValue(health.currentHealth, health.maxHealth);
    }

    public void TakeDamage(float amount, GameObject source)
    {
        if (immune) return;
        SoundManager.PlaySound(SoundType.PlayerHit);
        health.currentHealth -= amount;
        //Debug.Log($"{gameObject.name} took {amount} from {source}");
        if (health.currentHealth <= 0)
        {
            health.currentHealth = 0;
            Die();
        }
        HealthBarNewValue();
        animator.SetTrigger("Got Hit");
    }

    public void HealthBarNewValue()
    {
        healthBar.SetNewValue(health.currentHealth, health.maxHealth);
    } 

    void Die()
    {
        isDead = true;
        animator.SetBool("Dead", true);
    }
}
