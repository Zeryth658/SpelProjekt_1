using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public class Damping : MonoBehaviour
{
    private CinemachineConfiner2D confiner;
    [SerializeField] private LoseHealth loseHealth;

    void Awake()
    {
        confiner = GetComponent<CinemachineConfiner2D>();
    }    
    
    void Start()
    {
        loseHealth.Toggle();
        StartCoroutine(increaseDamping());
    }

    public IEnumerator increaseDamping()
    {
        yield return new WaitForSeconds(0.2f);
        
        confiner.Damping = 1;
        confiner.SlowingDistance = 1;
    }
}
