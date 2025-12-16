using System;
using System.Collections;
using System.Security.Permissions;
using UnityEngine;

public class LoseHealth : MonoBehaviour
{
    [Header("Health Reduction")]
    [SerializeField] private float damageInterval = 3f;
    [SerializeField] private int damageAmount = 1;
    [SerializeField] private float hpReductionThreshold = 5f;
    
    [Header("Speed Boost")]
    [SerializeField] private float hpSpeedThreshold = 12f;
    private float totalSpeedIncrease;
    [SerializeField] private float baseSpeedIncrease = 8f;
    private float moreSpeedIncrease;

    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private PlayerMovement playerMovement;

    private Coroutine damageCoroutine;

    private void FixedUpdate()
    {
        if (playerHealth.currentHealth > hpReductionThreshold)
        {
            StartDamageTimer();

            if (playerHealth.currentHealth > hpSpeedThreshold)
            {
                moreSpeedIncrease = playerHealth.currentHealth - (hpSpeedThreshold + 1);

                totalSpeedIncrease = baseSpeedIncrease + moreSpeedIncrease;
                
                playerMovement.maxSpeed = totalSpeedIncrease;
            }
            else
            {
                playerMovement.maxSpeed = playerMovement.standardMaxSpeed;
            }
        }
        else
        {
            StopDamageTimer();
        }
    }

    private void StartDamageTimer()
    {
        if(damageCoroutine == null)
        {
            damageCoroutine = StartCoroutine(TakeDamageOverTime());
        }
    }

    private void StopDamageTimer()
    {
        if(damageCoroutine != null)
        {
            StopCoroutine(damageCoroutine);
            damageCoroutine = null;
        }
    }

    private IEnumerator TakeDamageOverTime()
    {
        while(playerHealth.currentHealth > hpReductionThreshold)
        {
            yield return new WaitForSeconds(damageInterval);
            TakeDamage(damageAmount);
        }

        damageCoroutine = null;
    }

    private void TakeDamage(float amount)
    {        
        playerHealth.currentHealth -= amount;
        playerHealth.HealthBarNewValue();
    }
}