using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
public class PauseMenu : MonoBehaviour
{

    public GameObject pauseMenu;
    public GameObject optionsMenu;
    public GameObject generalMenu;
    public GameObject controlsMenu;
    public GameObject videoMenu;
    public PlayerInput playerInput;
    public GameObject audioMenu;

    
    
    [Header("First Selected Buttons")]
    public GameObject pauseFirstButton;
    public GameObject optionsFirstButton;
    public GameObject generalFirstButton;
    public GameObject controlsFirstButton;
    public GameObject videoFirstButton;
    public GameObject audioFirstButton;

    public static bool IsPaused { get; set; }
    public InputActionAsset actions; 
    private InputActionMap gameplayMap;
    private InputActionMap uiMap;
    [SerializeField] private InputActionReference cancel;
    [SerializeField] private InputActionReference pause;
    void Start()
    {
        pauseMenu.SetActive(false);
        optionsMenu.SetActive(false);
        generalMenu.SetActive(false);
        controlsMenu.SetActive(false);
        videoMenu.SetActive(false);
        audioMenu.SetActive(false);
    }
    void Awake()
    {
        gameplayMap = actions.FindActionMap("Player");
        uiMap = actions.FindActionMap("UI");
        pause.action.Enable();
        if (playerInput == null)
        {
            playerInput = FindFirstObjectByType<PlayerInput>();
        }
        if (PlayerPrefs.HasKey("rebinds"))
        {
            playerInput.actions.LoadBindingOverridesFromJson(
                PlayerPrefs.GetString("rebinds"));
        }
    }
    
    private void OnEnable()
    {
        pause.action.performed += TogglePause;
        cancel.action.performed += OnCancel;
    }

    private void OnDisable()
    {
        pause.action.performed -= TogglePause;
        cancel.action.performed -= OnCancel;
    }

    // Update is called once per frame
    private void TogglePause(InputAction.CallbackContext context)
    {
        if (IsPaused)
            ResumeGame();
        else
            PauseGame();
    }

    public void OpenGeneral()
    {
        optionsMenu.SetActive(false);
        generalMenu.SetActive(true);
        SelectFirstButton(generalFirstButton);
    }

    public void CloseGeneral()
    {
        optionsMenu.SetActive(true);
        generalMenu.SetActive(false);
        SelectFirstButton(optionsFirstButton);
    }

    public void OpenControls()
    {
        optionsMenu.SetActive(false);
        controlsMenu.SetActive(true);
        SelectFirstButton(controlsFirstButton);
    }

    public void CloseControls()
    {
        optionsMenu.SetActive(true);
        controlsMenu.SetActive(false);
        SelectFirstButton(optionsFirstButton);
    }

    public void OpenVideo()
    {
        optionsMenu.SetActive(false);
        videoMenu.SetActive(true);
        SelectFirstButton(videoFirstButton);
    }

    public void CloseVideo()
    {
        optionsMenu.SetActive(true);
        videoMenu.SetActive(false);
        SelectFirstButton(optionsFirstButton);
    }

    public void OpenAudio()
    {
        optionsMenu.SetActive(false);
        audioMenu.SetActive(true);
        SelectFirstButton(audioFirstButton);
    }

    public void CloseAudio()
    {
        optionsMenu.SetActive(true);
        audioMenu.SetActive(false);
        SelectFirstButton(optionsFirstButton);
    }

    public void OpenSettings()
    {
        pauseMenu.SetActive(false);
        optionsMenu.SetActive(true);
        SelectFirstButton(optionsFirstButton);
    }

    public void CloseSettings()
    {
        optionsMenu.SetActive(false);
        pauseMenu.SetActive(true);
        SelectFirstButton(pauseFirstButton);
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
        gameplayMap.Disable();
        uiMap.Enable(); 
        Time.timeScale = 0;
        IsPaused = true;
        
        SelectFirstButton(pauseFirstButton);
    }
    
    public void ResumeGame()
    {
        optionsMenu.SetActive(false);
        pauseMenu.SetActive(false);
        generalMenu.SetActive(false);
        controlsMenu.SetActive(false);
        videoMenu.SetActive(false);
        audioMenu.SetActive(false);
        gameplayMap.Enable();
        //uiMap.Disable();
        Time.timeScale = 1;
        IsPaused = false;
    }
    
    private void SelectFirstButton(GameObject firstButton)
    {
        if (firstButton == null) return;

        EventSystem.current.SetSelectedGameObject(null); // Clear previous selection
        EventSystem.current.SetSelectedGameObject(firstButton);
    }
    
    private void OnCancel(InputAction.CallbackContext context)
    {
        if (!IsPaused) return;

        if (generalMenu.activeSelf || controlsMenu.activeSelf || videoMenu.activeSelf || audioMenu.activeSelf)
        {
            generalMenu.SetActive(false);
            controlsMenu.SetActive(false);
            videoMenu.SetActive(false);
            audioMenu.SetActive(false);
            optionsMenu.SetActive(true);
            SelectFirstButton(optionsFirstButton);
        }
        else if (optionsMenu.activeSelf)
        {
            optionsMenu.SetActive(false);
            pauseMenu.SetActive(true);
            SelectFirstButton(pauseFirstButton);
        }
        else if (pauseMenu.activeSelf)
        {
            ResumeGame();
        }
    }
    

}
