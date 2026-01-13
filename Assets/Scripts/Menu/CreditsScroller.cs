using UnityEngine;
using UnityEngine.SceneManagement;


public class CreditsScroller : MonoBehaviour
{
    public float scrollSpeed = 50f;
    public float waitTime = 4f;
    public float autoSkipAfter = 60f;
    private float time;
    
    
    private RectTransform rectTransform;
    private RectTransform pauseTarget;
    private bool isPaused;
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        pauseTarget = GetComponentInChildren<CreditsPauseMarker>()?.GetComponent<RectTransform>();
    }
    void Update()
    {
        time += Time.deltaTime;
        if (time < waitTime)
        {
            return;
        }
        if (Input.anyKeyDown || time > autoSkipAfter)
        {
            SceneManager.LoadScene("StartMenu");
        }

        if (isPaused) return;
        rectTransform.anchoredPosition += Vector2.up * scrollSpeed * Time.deltaTime;
        
        if (pauseTarget != null && IsAtCenter(pauseTarget))
        {
            isPaused = true;
        }
    }
    
    
    bool IsAtCenter(RectTransform target)
    {
        float screenCenterY = Screen.height / 2f;
        Vector3 worldPos = target.position;
        return Mathf.Abs(worldPos.y - screenCenterY) < 5f;
    }
}
