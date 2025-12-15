using System.Security.Permissions;
using UnityEngine;

public class LoseHealth : MonoBehaviour
{
    private bool lowHealth;
    private bool highHealth;

    [SerializeField] private float lessThanMaxHpThreshold = 2f;
    [SerializeField] private float damageAmount = 0.03f;
    [SerializeField] private float speedIncrease = 3f;

    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private PlayerMovement playerMovement;

    private void FixedUpdate()
    {
        if (playerHealth.currentHealth >= playerHealth.maxHealth - lessThanMaxHpThreshold)
        {
            highHealth = true;
        }
        else if (playerHealth.currentHealth <= 5)
        {
            highHealth = false;
            lowHealth = true;
        }
        else
        {
            lowHealth = false;
            highHealth = false;
        }

        if (lowHealth == false) TakeDamage();

        if (highHealth) SpeedBoost();
    }

    private void TakeDamage()
    {
        playerHealth.currentHealth -= damageAmount;
        playerHealth.HealthBarNewValue();
    }

    private void SpeedBoost()
    {
        playerMovement.maxSpeed += speedIncrease;
    }
}