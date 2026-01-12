using System.Diagnostics.CodeAnalysis;
using UnityEngine;

public class Narration : MonoBehaviour
{
    private bool hasPlayed = false;
    public AudioObject clipToPlay;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && hasPlayed == false)
        {
            Debug.Log("Narration triggered!");
            Vocals.instance.Say(clipToPlay);
            hasPlayed = true;
            Destroy(GetComponent<Collider2D>());
        }
    }

    private void Update()
    {
        if(hasPlayed)
        {
            Destroy(gameObject);
        }
    }
}
