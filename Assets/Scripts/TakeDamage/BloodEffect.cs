using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BloodEffect : MonoBehaviour
{
    [Header("Prefab Settings")]
    public GameObject blood1;
    public GameObject blood2;
    public int amountToSpawn = 3;
    public float glideDuration = 1f;

    [Header("Spawn Area")]
    public float radius = 3f;
    public float spreadAngle = 100f;

    [Header("References")]
    public Transform player;   // Assign in Inspector

    void Awake()
    {
        // Safety fallback if not assigned
        if (player == null)
        {
            GameObject p = GameObject.FindGameObjectWithTag("Player");
            if (p != null)
                player = p.transform;
        }
    }

    void Start()
    {
        SpawnAll();
    }

    void SpawnAll()
    {
        Transform[] spawned = new Transform[amountToSpawn];
        Vector3[] targets = new Vector3[amountToSpawn];

        for (int i = 0; i < amountToSpawn; i++)
        {
            targets[i] = GetBiasedPointAwayFromPlayer();

            Quaternion randomRotation = Quaternion.Euler(
                0f,
                0f,
                Random.Range(0f, 360f)
            );

            GameObject chosenPrefab = Random.value < 0.5f ? blood1 : blood2;

            spawned[i] = Instantiate(
                chosenPrefab,
                transform.position,
                randomRotation,
                transform
            ).transform;

            Vector2 dir = (targets[i] - transform.position).normalized;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            spawned[i].rotation = Quaternion.Euler(0f, 0f, angle);
        }

        for (int i = 0; i < amountToSpawn; i++)
        {
            StartCoroutine(GlideToPosition(spawned[i], targets[i]));
        }
    }


    IEnumerator GlideToPosition(Transform obj, Vector3 target)
    {
        Vector3 start = obj.position;
        float elapsed = 0f;

        while (elapsed < glideDuration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / glideDuration;
            t = Mathf.SmoothStep(0f, 1f, t);

            obj.position = Vector3.Lerp(start, target, t);
            yield return null;
        }

        obj.position = target;
    }

    Vector3 GetBiasedPointAwayFromPlayer()
    {
        if (player == null)
            return GetRandomPointInCircle();

        Vector2 baseDir = ((Vector2)transform.position - (Vector2)player.position).normalized;

        float randomAngle = Random.Range(-spreadAngle * 0.5f, spreadAngle * 0.5f);
        Vector2 finalDir = Quaternion.Euler(0f, 0f, randomAngle) * baseDir;

        float distance = Random.Range(radius * 0.5f, radius);

        return transform.position + (Vector3)(finalDir * distance);
    }

    Vector3 GetRandomPointInCircle()
    {
        float angle = Random.Range(0f, Mathf.PI * 2f);
        float dist = Mathf.Sqrt(Random.Range(0f, 1f)) * radius;

        return transform.position + new Vector3(
            Mathf.Cos(angle) * dist,
            Mathf.Sin(angle) * dist,
            0f
        );
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        int segments = 64;
        Vector3 prevPoint = transform.position + Vector3.right * radius;

        for (int i = 1; i <= segments; i++)
        {
            float angle = i * Mathf.PI * 2f / segments;
            Vector3 nextPoint = transform.position +
                                new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * radius;

            Gizmos.DrawLine(prevPoint, nextPoint);
            prevPoint = nextPoint;
        }
    }
}