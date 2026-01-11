using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;


public class MainMenu : MonoBehaviour
{
    public Image fadeImage;
    public float fadeDuration = 1.5f;
    bool isFading;

    public void PlayGame()
    {
        if (!isFading)
        {
            StartCoroutine(FadeAndLoad());
        }
    }

    public void QuitGame()
    {
        Debug.Log("QUIT!");
        Application.Quit();
    }

    IEnumerator FadeAndLoad()
    {
        Color c = fadeImage.color;
        c.a = 0f;
        fadeImage.color = c;

        float t = 0f;

        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            c.a = t / fadeDuration;
            fadeImage.color = c;
            yield return null;
        }

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
