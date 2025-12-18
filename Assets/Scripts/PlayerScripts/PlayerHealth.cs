using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    public float maxHealth = 15f;
    public float currentHealth = 5f;
    public HealthBar healthBar;
    public Hurtbox hurtbox;
    private Animator animator;

    public bool immune = false;
    public bool isDead = false;

    [SerializeField] private AudioSource playerGetHitSound1;
    [SerializeField] private AudioSource playerGetHitSound2;

    public void Awake()
    {
        isDead = false;
        animator = GetComponentInChildren<Animator>();
    }

    public void Start()
    {
        healthBar.SetValue(currentHealth, maxHealth);
    }

    public void TakeDamage(float amount, GameObject source)
    {
        if (immune) return;
        PlayGetHitSounds();
        currentHealth -= amount;
        Debug.Log($"{gameObject.name} took {amount} from {source}");
        if (currentHealth <= 0)
        {
            Die();
        }
        HealthBarNewValue();
        animator.SetTrigger("Got Hit");
    }

    public void HealthBarNewValue()
    {
        healthBar.SetNewValue(currentHealth, maxHealth);
    } 

    void Die()
    {
        isDead = true;
        animator.SetBool("Dead", true);
    }

    void PlayGetHitSounds()
    {
        playerGetHitSound1.Play();
        playerGetHitSound2.Play();
    }
}
