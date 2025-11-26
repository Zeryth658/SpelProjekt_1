using UnityEngine;

public class EnemyHealth : MonoBehaviour, IDamageable
{
    public float health = 5f;
    public Hurtbox hurtbox;

    public void TakeDamage(float amount, GameObject source)
    {
        health -= amount;
        Debug.Log($"{gameObject.name} took {amount} from {source}");
        if (health <= 0)
            Die();
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
