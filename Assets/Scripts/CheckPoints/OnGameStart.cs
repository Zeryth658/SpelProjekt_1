using UnityEngine;

public class OnGameStart : MonoBehaviour
{
    void Start()
    {
        
        if (GameManager.Instance.lastCheckpoint == Vector2.zero)
            GameManager.Instance.lastCheckpoint = transform.position;
       
        transform.position = GameManager.Instance.lastCheckpoint;
    }
}
