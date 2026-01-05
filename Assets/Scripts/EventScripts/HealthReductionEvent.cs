using UnityEngine;

public class HealthReductionEvent : MonoBehaviour
{
    [SerializeField] private LoseHealth loseHealth;
    private TagHandle playerTag;
    private bool performed = false;

    public void OnEnable()
    {
        playerTag = TagHandle.GetExistingTag("Player");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (performed) { return; }

        if (other.CompareTag(playerTag))
        {
            loseHealth.Toggle();
            performed = true;
        }
    }
}
