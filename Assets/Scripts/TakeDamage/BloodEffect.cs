using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BloodEffect : MonoBehaviour
{
    [Header("Prefab Settings")]
    public GameObject prefab;
    public int amountToSpawn = 3;
    public float delay = 0.5f;

    [Header("Spawn Area")]
    public float radius = 3f;

    void Start()
    {
        StartCoroutine(SpawnAfterDelay());
    }

    IEnumerator SpawnAfterDelay()
    {
        yield return new WaitForSeconds(delay);

        for (int i = 0; i < amountToSpawn; i++)
        {
            Vector3 randomPos = GetRandomPointInCircle();
            Instantiate(prefab, randomPos, Quaternion.identity);
        }
    }

    Vector3 GetRandomPointInCircle()
    {
        // Random point inside a circle (uniform distribution)
        float angle = Random.Range(0f, Mathf.PI * 2f);
        float dist = Mathf.Sqrt(Random.Range(0f, 1f)) * radius;

        float x = Mathf.Cos(angle) * dist;
        float y = Mathf.Sin(angle) * dist;

        return transform.position + new Vector3(x, y, 0f);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;

        // Draw the circle (Unity doesn't have a built-in circle, so approximate it)
        int segments = 64;
        Vector3 prevPoint = transform.position + new Vector3(radius, 0, 0);

        for (int i = 1; i <= segments; i++)
        {
            float angle = (i * Mathf.PI * 2f) / segments;
            Vector3 newPoint = transform.position + new Vector3(Mathf.Cos(angle) * radius, Mathf.Sin(angle) * radius, 0);

            Gizmos.DrawLine(prevPoint, newPoint);
            prevPoint = newPoint;
        }
    }
}
//public ParticleSystem particleSystem;
//public GameObject prefab;
//public int numberToSpawn = 3;
//public float delay = 0.5f;

//private ParticleSystem.Particle[] particles;

//void Start()
//{
//    particles = new ParticleSystem.Particle[particleSystem.main.maxParticles];
//    StartCoroutine(SpawnPrefabsAfterDelay());
//}

//IEnumerator SpawnPrefabsAfterDelay()
//{
//    // Wait for the delay
//    yield return new WaitForSeconds(delay);

//    int alive = particleSystem.GetParticles(particles);

//    // If fewer than 3 particles exist, spawn on as many as we have
//    int count = Mathf.Min(numberToSpawn, alive);

//    for (int i = 0; i < count; i++)
//    {
//        // Convert particle local position → world position
//        Vector3 worldPos = particleSystem.transform.TransformPoint(particles[i].position);

//        Instantiate(prefab, worldPos, Quaternion.identity);
//    }
//}