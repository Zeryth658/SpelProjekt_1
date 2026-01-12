using System;
using UnityEngine;

public class HealthUp : MonoBehaviour
{
    [SerializeField] private ParticleSystem healEffect;

    public void Update()
    {
        if (!healEffect.isPlaying)
        {
            PoolManager.Despawn(gameObject);
            return;
        }
    }
}
