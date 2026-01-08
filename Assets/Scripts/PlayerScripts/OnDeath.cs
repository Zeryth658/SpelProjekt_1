using System.Collections;
using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OnDeath : MonoBehaviour
{
    public void OnDeathAnimEvent()
    {  
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
