using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public Vector2 lastCheckpoint;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
}
