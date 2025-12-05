using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetGame : MonoBehaviour
{
    public Vector3 StartPos;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            string currentSceneName = SceneManager.GetActiveScene().name;
            GameManager.Instance.lastCheckpoint = StartPos;
            SceneManager.LoadScene(currentSceneName);
        }
    }
}
