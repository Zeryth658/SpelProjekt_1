using System.Diagnostics.CodeAnalysis;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Narration : MonoBehaviour
{
    private bool hasPlayed = false;
    AudioSource audioSource;
    Collider2D collider2D;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        collider2D = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Narration triggered!");
            audioSource.Play();
            hasPlayed = true;
            Destroy(GetComponent<Collider2D>());
        }
    }

    private void Update()
    {
        if(hasPlayed && !audioSource.isPlaying)
        {
            Destroy(gameObject);
        }
    }
}
