using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SearchService;

public class HealthRecovery : MonoBehaviour
{
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private int healAmount = 1;
    private TagHandle enemyTag;

    public void OnEnable()
    {
        enemyTag = TagHandle.GetExistingTag("Enemy");
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(enemyTag))
        {
            playerHealth.currentHealth += healAmount;
        }
    }
}
