using UnityEngine;
using Unity.Cinemachine;

public class CameraFollowSwitch : MonoBehaviour
{
    [SerializeField] private bool isFollowing = false;
    private CinemachineCamera cinCamera; 
    
    private CinemachineFollow cinemachineFollow;
    
    private void Awake()
    {
        cinCamera = FindFirstObjectByType<CinemachineCamera>();
        cinemachineFollow = cinCamera.GetComponent<CinemachineFollow>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            cinemachineFollow.enabled = isFollowing;
        }
    }
}