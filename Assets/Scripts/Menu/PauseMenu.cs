using UnityEngine;
using UnityEngine.SceneManagement;
public class PauseMenu : MonoBehaviour
{

    public GameObject pauseMenu;
    public GameObject optionsMenu;
    public static bool IsPaused { get; set; }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pauseMenu.SetActive(false);
        optionsMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (IsPaused) { ResumeGame(); }
            else { PauseGame(); }
        }
        
    }

    public void OpenSettings()
    {
        optionsMenu.SetActive(true);
    }
    public void RestartGame()
    {
        ResumeGame();
        ResetGame.Reset();
    }
    public void QuitGame()
    {
        ResumeGame();
        SceneManager.LoadScene(0);
    }

    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
        IsPaused = true;
    }
    
    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
        IsPaused = false;
    }
}
