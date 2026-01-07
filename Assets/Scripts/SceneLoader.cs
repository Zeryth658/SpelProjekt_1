using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private string sceneToLoad;

    private TagHandle playerTag;

    private bool performed = false;

    public void OnEnable()
    {
        playerTag = TagHandle.GetExistingTag("Player");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (performed) { return; }

        if (other.CompareTag(playerTag))
        {
            if (!string.IsNullOrEmpty(sceneToLoad))
            {
               SceneManager.LoadScene(sceneToLoad); 
            }
            else
            {
                Debug.LogWarning("");
            }
        }
    }
}
