using System;
using System.Collections;
using UnityEngine;

public class LoseHealth : MonoBehaviour
{
    [Header("Health Reduction")]
    public bool takeDamage = false;
    [SerializeField] private float damageInterval = 3f;
    [SerializeField] private int damageAmount = 1;
    [SerializeField] private float hpReductionThreshold = 5f;

    
    [Header("Speed Boost")]
    [SerializeField] private float hpSpeedThreshold = 12f;
    [SerializeField] private float speedIncrease = 3f;
    [SerializeField] private float speedAudioDuration = 0.5f;
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private ParticleSystem speedEffect;
    [SerializeField] private AudioSource speedAudio;

    [Header("Low Health Indicator")]
    [SerializeField] private float lowHealthIndicatorThreshold = 50f;
    [SerializeField] private CanvasGroup healthIndicator;
    [SerializeField] private float indicatorFadeDuration = 0.5f;
    
    private Coroutine healthIndicatorCoroutine;
    private Coroutine damageCoroutine;
    private Coroutine speedVolumeCoroutine;
    private bool speedBoostActive = false;
    private bool healthIndicatorActive = false;
    
    private void FixedUpdate()
    {
        if (takeDamage && playerHealth.currentHealth > hpReductionThreshold)
        {
            StartDamageTimer();

            if (playerHealth.currentHealth > hpSpeedThreshold)
            { 
                playerMovement.maxSpeed = playerMovement.standardMaxSpeed + speedIncrease;
                
                if (!speedEffect.isPlaying) 
                { 
                    Debug.Log("Start Speed Effect");
                    speedEffect.Play(); 
                }
            }
            else
            {
                playerMovement.maxSpeed = playerMovement.standardMaxSpeed;
                if(speedEffect.isPlaying) 
                { 
                    Debug.Log("Stop Speed Effect");
                    speedEffect.Stop();
                }
            }
        }
        else
        {
            StopDamageTimer();
        }
        bool shouldHaveHealthIndicator = playerHealth.currentHealth < lowHealthIndicatorThreshold;
        if (shouldHaveHealthIndicator != healthIndicatorActive)
        {
            healthIndicatorActive = shouldHaveHealthIndicator;
            ActivateHealthIndicator();
        }
        bool shouldHaveSpeedBoost = playerHealth.currentHealth > hpSpeedThreshold;
        if (shouldHaveSpeedBoost != speedBoostActive)
        {
            speedBoostActive = shouldHaveSpeedBoost;
            OnSpeedBoostStateChanged(speedBoostActive);
        }
    }

    private void Update()
    {
        if (healthIndicatorActive && healthIndicator.alpha > 0.01f)
        {
            healthIndicator.alpha = Mathf.Lerp(0.7f, 1f, Mathf.PingPong(Time.time, 1f));
        }
    }

    private void ActivateHealthIndicator()
    {
        if (healthIndicatorCoroutine != null)
            StopCoroutine(healthIndicatorCoroutine);

        float targetAlpha = healthIndicatorActive ? 1f : 0f;
        healthIndicatorCoroutine = StartCoroutine(FadeHealthIndicator(targetAlpha));
    }
    
    private IEnumerator FadeHealthIndicator(float targetAlpha)
    {
        float startAlpha = healthIndicator.alpha;
        float time = 0f;

        while (time < indicatorFadeDuration)
        {
            time += Time.deltaTime;
            healthIndicator.alpha = Mathf.Lerp(startAlpha, targetAlpha, time / indicatorFadeDuration);
            yield return null;
        }

        healthIndicator.alpha = targetAlpha;
    }

    private void OnSpeedBoostStateChanged(bool active)
    {
        if (active)
        {
            SetSpeedAudioVolume(0.6f);
        }
        else
        {
            SetSpeedAudioVolume(0f);
        }
    }

    private void SetSpeedAudioVolume(float targetVolume)
    {
        if (speedAudio == null) return;
        if (speedVolumeCoroutine != null)
            StopCoroutine(speedVolumeCoroutine);

        speedVolumeCoroutine = StartCoroutine(DynamicSpeedVolume(targetVolume));
    }

    private void OnDestroy()
    {
        if (speedVolumeCoroutine != null)
        {
            StopCoroutine(speedVolumeCoroutine);
            speedVolumeCoroutine = null;
        }
    }
    private IEnumerator DynamicSpeedVolume(float targetVolume)
    {
        
        float startVolume = speedAudio.volume;
        float time = 0;
        while (time < speedAudioDuration)
        {
            time += Time.deltaTime;
            speedAudio.volume = Mathf.Lerp(startVolume, targetVolume, time / speedAudioDuration);
            yield return null;
        }
        speedAudio.volume = targetVolume;
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

        if (playerHealth.currentHealth <= 0)
        {
            playerHealth.currentHealth = 0;
        }

        playerHealth.HealthBarNewValue();
    }

    public void Toggle()
    {
        takeDamage = !takeDamage;
    }
}