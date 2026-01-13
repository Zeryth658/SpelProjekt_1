
using UnityEngine;
using UnityEngine.SceneManagement;

public class StopPersistentMusic : MonoBehaviour
{
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (MusicManager.Instance != null)
            Destroy(MusicManager.Instance.gameObject);
    }
}