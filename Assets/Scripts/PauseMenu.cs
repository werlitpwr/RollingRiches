using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject PauseMenuUI;       // Reference to the main pause menu UI
    public GameObject SettingsPanel;  // Reference to the settings panel
    public GameObject ExitConfirmationPanel;      // Reference to the exit confirmation panel
    private bool isPaused = false;

    void Start()
    {
        

        // Ensure all panels are hidden at the start
        PauseMenuUI.SetActive(false);
        SettingsPanel.SetActive(false);
        ExitConfirmationPanel.SetActive(false);
    }

    void Update()
    {
        // Toggle pause menu with the P key
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (isPaused)
                Resume();
            else
                Pause();
        }
    }

    public void TogglePause()
    {
        if (isPaused)
        {
            PauseMenuUI.SetActive(false);
            SettingsPanel.SetActive(false);
            ExitConfirmationPanel.SetActive(false);
            Time.timeScale = 1f; // Resume game time
            isPaused = false;
        }
        else
        {
            PauseMenuUI.SetActive(true);
            Time.timeScale = 0f; // Freeze game time
            isPaused = true;
        }
    }

    public void Pause()
    {
        PauseMenuUI.SetActive(true); // Show pause menu
        Time.timeScale = 0f;        // Freeze game time
        isPaused = true;
    }

    public void Resume()
    {
        PauseMenuUI.SetActive(false); // Hide pause menu
        SettingsPanel.SetActive(false); // Ensure settings panel is hidden
        ExitConfirmationPanel.SetActive(false);    // Ensure exit confirmation panel is hidden
        Time.timeScale = 1f;             // Resume game time
        isPaused = false;
    }

    public void OpenSettings()
    {
        PauseMenuUI.SetActive(false);  // Hide pause menu
        SettingsPanel.SetActive(true); // Show settings panel
    }

    public void CloseSettings()
    {
        SettingsPanel.SetActive(false); // Hide settings panel
        PauseMenuUI.SetActive(true);      // Return to pause menu
    }

    public void OpenExitConfirmation()
    {
        PauseMenuUI.SetActive(false);  // Hide pause menu
        ExitConfirmationPanel.SetActive(true);   // Show exit confirmation panel
    }

    public void CancelExit()
    {
        ExitConfirmationPanel.SetActive(false); // Hide exit confirmation panel
        PauseMenuUI.SetActive(true);  // Return to pause menu
    }

    public void QuitToMainMenu()
    {
        Time.timeScale = 1f; // Ensure game time is running
        SceneManager.LoadScene("MainMenu"); // Load the Main Menu scene
    }
}
