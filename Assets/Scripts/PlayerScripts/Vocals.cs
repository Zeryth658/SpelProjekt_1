using UnityEngine;

public class Vocals : MonoBehaviour
{
    [SerializeField] private AudioSource source;
    public static Vocals instance;

    private void Awake()
    {
        instance = this;
    }

    public void Say(AudioObject narrate)
    {
        source.Stop();

        source.clip = narrate.clip;
        source.Play();

        UI.instance.SetSubtitle(narrate.subtitle, narrate.clip.length);
    }
}
