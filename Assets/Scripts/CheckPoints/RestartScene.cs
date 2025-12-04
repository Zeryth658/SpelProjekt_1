using UnityEngine;
using UnityEngine.SceneManagement;
public class RestartScene : MonoBehaviour
{
  
    public void ResetLevel()
    { 
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
}
