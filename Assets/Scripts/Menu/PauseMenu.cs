using UnityEngine;
using UnityEngine.SceneManagement;
public class PauseMenu : MonoBehaviour
{

    public GameObject pauseMenu;
    public GameObject optionsMenu;
    public GameObject generalMenu;
    public GameObject controlsMenu;
    public GameObject videoMenu;
    public GameObject audioMenu;
    public static bool IsPaused { get; set; }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pauseMenu.SetActive(false);
        optionsMenu.SetActive(false);
        generalMenu.SetActive(false);
        controlsMenu.SetActive(false);
        videoMenu.SetActive(false);
        audioMenu.SetActive(false);
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

    public void OpenGeneral()
    {
        optionsMenu.SetActive(false);
        generalMenu.SetActive(true);
    }

    public void CloseGeneral()
    {
        optionsMenu.SetActive(true);
        generalMenu.SetActive(false);
    }

    public void OpenControls()
    {
        optionsMenu.SetActive(false);
        controlsMenu.SetActive(true);
    }

    public void CloseControls()
    {
        optionsMenu.SetActive(true);
        controlsMenu.SetActive(false);
    }

    public void OpenVideo()
    {
        optionsMenu.SetActive(false);
        videoMenu.SetActive(true);
    }

    public void CloseVideo()
    {
        optionsMenu.SetActive(true);
        videoMenu.SetActive(false);
    }

    public void OpenAudio()
    {
        optionsMenu.SetActive(false);
        audioMenu.SetActive(true);
    }

    public void CloseAudio()
    {
        optionsMenu.SetActive(true);
        audioMenu.SetActive(false);
    }

    public void OpenSettings()
    {
        pauseMenu.SetActive(false);
        optionsMenu.SetActive(true);
    }

    public void CloseSettings()
    {
        optionsMenu.SetActive(true);
        pauseMenu.SetActive(false);
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
        optionsMenu.SetActive(false);
        pauseMenu.SetActive(false);
        generalMenu.SetActive(false);
        controlsMenu.SetActive(false);
        videoMenu.SetActive(false);
        audioMenu.SetActive(false);
        Time.timeScale = 1;
        IsPaused = false;
    }
}
