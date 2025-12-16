using System.Security.Permissions;
using UnityEngine;

public class LoseHealth : MonoBehaviour
{
    private bool lowHealth;
    private bool highHealth;

    [SerializeField] private float hpThreshold = 12f;
    [SerializeField] private float damageAmount = 0.03f;
    [SerializeField] private float baseSpeedIncrease = 3f;
    //private float moreSpeedBoost;

    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private PlayerMovement playerMovement;

    private void FixedUpdate()
    {
        if (playerHealth.currentHealth > 5)
        {
            lowHealth = false;

            if (playerHealth.currentHealth > hpThreshold)
            {
                //moreSpeedBoost = playerHealth.currentHealth - (hpThreshold + 1);
                highHealth = true;
            }
            else
            {
                highHealth = false;
            }
        }
        else if (playerHealth.currentHealth <= 5)
        {
            highHealth = false;
            lowHealth = true;
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
        playerMovement.maxSpeed += baseSpeedIncrease;
        //+ moreSpeedBoost;
    }
}