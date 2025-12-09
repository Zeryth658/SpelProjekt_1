using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Tilemaps;

[DisallowMultipleComponent]
[RequireComponent(typeof(ParticleSystem))]
public class BloodEffect : MonoBehaviour
{
    public Vector3 minBloodScale = new Vector3(0.5f, 0.5f, 0.5f),
        maxBloodScale = new Vector3(1f, 1f, 1f);
    public Gradient bloodColors;
    [Range(0,1)] public float ratio = 0.34f;

    List<ParticleCollisionEvent> collisionEvents = new List<ParticleCollisionEvent>();
    ParticleSystem particles;

    void Start() 
    {
        particles = GetComponent<ParticleSystem>();
    }

    void OnParticleCollision(GameObject other) 
    {
        Debug.Log("WORKING");
        if(!other.TryGetComponent(out TilemapRenderer t)) return;
        if(Random.value > ratio) return;

        // We have to cycle through all collision events.
        int numCollisionEvents = particles.GetCollisionEvents(other, collisionEvents);
        for (int i = 0; i < numCollisionEvents; i++) {

            // Only consider collisions that hit our component.
            if(collisionEvents[i].colliderComponent.gameObject == other) 
            {
                Vector2 sfxPos = collisionEvents[i].intersection;
                ParticleSystemManager.CreateBloodStain(
                    other.transform, sfxPos, 
                    bloodColors.Evaluate(Random.Range(0, 1)),
                    new Vector3(
                        Random.Range(minBloodScale.x, maxBloodScale.x),
                        Random.Range(minBloodScale.y, maxBloodScale.y),
                        Random.Range(minBloodScale.z, maxBloodScale.z)
                    )
                );
            }
        }
    }

    // Ensure minBloodScale does not exceed maxBloodScale.
    void OnValidate() 
    {
        minBloodScale = new Vector3(
            Mathf.Min(minBloodScale.x, maxBloodScale.x),
            Mathf.Min(minBloodScale.y, maxBloodScale.y),
            Mathf.Min(minBloodScale.z, maxBloodScale.z)
        );
    }
}
