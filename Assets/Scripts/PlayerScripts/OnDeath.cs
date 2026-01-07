using System.Collections;
using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OnDeath : MonoBehaviour
{
    [SerializeField] private CinemachineConfiner2D confiner;

    public void OnDeathAnimEvent()
    {  
        confiner.Damping = 0;
        confiner.SlowingDistance = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        
        StartCoroutine(Damping());
    }

    public IEnumerator Damping()
    {
        yield return new WaitForNextFrameUnit();
        
        confiner.Damping = 1;
        confiner.SlowingDistance = 1;
    }
}
