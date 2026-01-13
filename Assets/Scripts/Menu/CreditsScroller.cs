using UnityEngine;
using UnityEngine.SceneManagement;


public class CreditsScroller : MonoBehaviour
{
    public float scrollSpeed = 50f;
    public float waitTime = 4f;
    private float time;
    void Update()
    {
        if (Input.anyKeyDown)
        {
            SceneManager.LoadScene("MainMenu");
        }
        if (time < waitTime)
        {
            time += Time.deltaTime;
            return;
        }
        transform.Translate(Vector3.up * scrollSpeed * Time.deltaTime);
    }
}
