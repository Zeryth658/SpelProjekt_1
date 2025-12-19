using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetGame : MonoBehaviour
{
    private static Vector3 StartPos;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartPos = transform.position;
    }


    public static void Reset()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        GameManager.Instance.lastCheckpoint = GameManager.Instance.startPoint;
        SceneManager.LoadScene(currentSceneName);
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
          Reset();
        }
    }
}
