using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerHealth : MonoBehaviour, IDamageable
{
    public float maxHealth = 5f;
    public float currentHealth;
    public HealthBar healthBar;
    public Hurtbox hurtbox;
    private Animator animator;

    [SerializeField] private AudioSource playerGetHitSound1;
    [SerializeField] private AudioSource playerGetHitSound2;

    public void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    public void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetValue(currentHealth, maxHealth);
    }

    public void TakeDamage(float amount, GameObject source)
    {
        PlayGetHitSounds();
        currentHealth -= amount;
        Debug.Log($"{gameObject.name} took {amount} from {source}");
        if (currentHealth <= 0)
        {
            Die();
        }
        healthBar.SetNewValue(currentHealth, maxHealth);
        animator.SetTrigger("Got Hit");

    }

    void Die()
    {
        animator.SetBool("Dead", true);
    }

    void PlayGetHitSounds()
    {
        playerGetHitSound1.Play();
        playerGetHitSound2.Play();
    }
}
