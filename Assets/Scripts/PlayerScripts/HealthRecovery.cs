using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SearchService;

public class HealthRecovery : MonoBehaviour
{
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private int healAmount = 1;
    private TagHandle enemyTag;

    private void OnEnable()
    {
        enemyTag = TagHandle.GetExistingTag("Enemy");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(enemyTag))
        {
            playerHealth.currentHealth += healAmount;
        }
    }
}
