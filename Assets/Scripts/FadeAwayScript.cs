using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FadeAwayScript : MonoBehaviour
{
    public float delayBeforeFade = 3f;
    public float fadeDuration = 1f;

    [SerializeField] private Image image;
    [SerializeField] private TMP_Text tmpText;

    private Color originalTextColor;

    void Awake()
    {
        if (image == null)
            image = GetComponent<Image>();

        if (tmpText != null)
            originalTextColor = tmpText.color;
    }

    void Start()
    {
        StartCoroutine(FadeRoutine());
    }

    IEnumerator FadeRoutine()
    {
        // ---- DELAY PHASE ----
        if (tmpText != null)
            tmpText.color = Color.white;

        yield return new WaitForSeconds(delayBeforeFade);

        // Restore original text color
        if (tmpText != null)
            tmpText.color = originalTextColor;

        // ---- FADE PHASE ----
        Color c = image.color;
        float startAlpha = c.a;
        float t = 0f;

        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            c.a = Mathf.Lerp(startAlpha, 0f, t / fadeDuration);
            image.color = c;
            yield return null;
        }

        Destroy(gameObject);
    }
}
