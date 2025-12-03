using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    public float maxHealth = 5f;
    public float currentHealth;

    public Hurtbox hurtbox;

    public void Start()
    {
        currentHealth = maxHealth;
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
        Destroy(gameObject);
    }
}
