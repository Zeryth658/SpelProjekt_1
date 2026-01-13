using UnityEngine;

[CreateAssetMenu(menuName = "Player/HealthData")]
public class HealthData : ScriptableObject
{
    public float maxHealth = 150;
    public float lessThanMax = 50;

    [HideInInspector] 
    public float currentHealth;

    public void OnEnable()
    {
        currentHealth = maxHealth -= lessThanMax;
    }
}
