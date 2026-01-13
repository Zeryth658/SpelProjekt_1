using UnityEngine;
using UnityEngine.SceneManagement;


public class CreditsScroller : MonoBehaviour
{
    public float scrollSpeed = 50f;
    public float waitTime = 4f;
    public float autoSkipAfter = 60f;
    private float time;
    void Update()
    {
        time += Time.deltaTime;
        if (Input.anyKeyDown || time > autoSkipAfter)
        {
            SceneManager.LoadScene("StartMenu");
        }
        if (time < waitTime)
        {
            return;
        }
        transform.Translate(Vector3.up * scrollSpeed * Time.deltaTime);
    }
}
