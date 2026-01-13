using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FadeToBlackScript : MonoBehaviour
{
    
    private TagHandle playerTag;
    private bool collided = false;
    private bool startTimeFade = false;
    private float objectX;
    
    private Transform playerTransform;

    [SerializeField] private Image fadeImage;
    [SerializeField] private float distanceFadeSpeed;
    [SerializeField] private float transitionToTimerPoint;
    [SerializeField] private float timedFadeSpeed;
    [SerializeField] private string sceneToLoad;
    
    void Start()
    {
        objectX = transform.position.x;
        playerTag = TagHandle.GetExistingTag("Player");
    }

    void Update()
    {
        if (collided)
        {
            Color fadeImageColor = fadeImage.color;
            
            if (startTimeFade)
            {
                fadeImageColor.a += Time.deltaTime * timedFadeSpeed;
            }
            else if (fadeImageColor.a >= 1)
            {
                SceneManager.LoadScene(sceneToLoad);
            }
            else
            {
                fadeImageColor.a = (playerTransform.position.x - objectX) * distanceFadeSpeed;
                if (fadeImageColor.a > transitionToTimerPoint)
                {
                    startTimeFade = true;
                }
            }
            fadeImage.color = fadeImageColor;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(playerTag))
        {
            playerTransform = other.transform;
            collided = true;
        }
    }
}
