using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    public GameObject settingsPanelUI; // Reference to the settings panel
    public Toggle soundToggle;         // Reference to the sound toggle
    public Button backButton;          // Reference to the back button
    public Button ballColorButton;     // Reference to the ball color change button (for later)

    private bool isSoundOn = true;

    void Start()
    {
        // Initialize sound based on the toggle value
        soundToggle.isOn = isSoundOn;

        // Set up listeners for the buttons and toggle
        soundToggle.onValueChanged.AddListener(ToggleSound);
        backButton.onClick.AddListener(GoBackToPauseMenu);
        ballColorButton.onClick.AddListener(ChangeBallColor); // Will implement later

        // Initially, hide the settings panel
        settingsPanelUI.SetActive(false);
    }

    // Function to toggle sound on and off
    void ToggleSound(bool isOn)
    {
        isSoundOn = isOn;
        if (isSoundOn)
        {
            // Enable sound
            AudioListener.volume = 1f; // Set sound volume to 1 (full volume)
        }
        else
        {
            // Disable sound
            AudioListener.volume = 0f; // Set sound volume to 0 (mute)
        }
    }

    // Function to go back to the pause menu
    void GoBackToPauseMenu()
    {
        settingsPanelUI.SetActive(false); // Hide settings panel
        // Show the pause menu (assuming you have a reference to it in your PauseMenu script)
        // You might want to use the PauseMenu's method here to show it again.
        // Example: pauseMenuUI.SetActive(true);
    }

    // Placeholder for changing ball color (to be implemented later)
    void ChangeBallColor()
    {
        // Later, implement functionality to change the ball's color
        Debug.Log("Ball Color Change functionality is not yet implemented.");
    }
}

