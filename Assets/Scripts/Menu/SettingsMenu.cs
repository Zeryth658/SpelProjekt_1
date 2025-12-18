using UnityEngine;
using UnityEngine.SceneManagement;
public class SettingsMenu : MonoBehaviour
{

    public GameObject optionsMenu;
    public GameObject videoMenu;
    public GameObject audioMenu;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        optionsMenu.SetActive(false);
        videoMenu.SetActive(false);
        audioMenu.SetActive(false);
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
        optionsMenu.SetActive(true);
    }

    public void CloseSettings()
    {
        optionsMenu.SetActive(false);
    }

}
