using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private string sceneToLoad;
    public Vector2 spawnPosition;

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
               GameManager.Instance.lastCheckpoint = spawnPosition + new Vector2(0, other.transform.position.y - transform.position.y);
            }
            else
            {
                Debug.LogWarning("");
            }
        }
    }
}
