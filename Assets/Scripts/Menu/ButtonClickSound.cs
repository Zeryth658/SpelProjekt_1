using UnityEngine;
using UnityEngine.UI;

public class ButtonClickSound : MonoBehaviour
{
    public AudioClip clickSound;
    private AudioSource audioSource;

    void Awake()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;

        Button[] buttons = FindObjectsByType<Button>(
            FindObjectsInactive.Include,
            FindObjectsSortMode.None
        );

        foreach (Button button in buttons)
        {
            button.onClick.AddListener(PlayClickSound);
        }
    }

    void PlayClickSound()
    {
        audioSource.PlayOneShot(clickSound);
    }
}