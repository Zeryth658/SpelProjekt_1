using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerHealth : MonoBehaviour, IDamageable
{
    public float maxHealth = 5f;
    public float currentHealth;

    public HealthBar healthBar;

    public Hurtbox hurtbox;

    public void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    public void TakeDamage(float amount, GameObject source)
    {
        currentHealth -= amount;
        Debug.Log($"{gameObject.name} took {amount} from {source}");
        if (currentHealth <= 0)
        {
            Die();
        }
        healthBar.SetHealth(currentHealth);
    }

    void Die()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
