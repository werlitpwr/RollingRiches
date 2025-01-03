using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    public GameObject settingsPanel;   // Reference to the settings panel
    public Toggle soundToggle;         // Toggle for enabling/disabling sound
    public Slider volumeSlider;        // Slider for adjusting volume
    public Button backButton;          // Button to return to the pause menu
    
    private bool isSoundOn = true;
    private int currentBallColorIndex = 0;

    void Start()
    {
        // Load sound settings
        isSoundOn = PlayerPrefs.GetInt("Sound", 1) == 1;
        soundToggle.isOn = isSoundOn;
        AudioListener.volume = isSoundOn ? PlayerPrefs.GetFloat("Volume", 1f) : 0f;

        // Load volume settings
        volumeSlider.value = PlayerPrefs.GetFloat("Volume", 1f);

        // Set up UI listeners
        soundToggle.onValueChanged.AddListener(ToggleSound);
        volumeSlider.onValueChanged.AddListener(AdjustVolume);
        backButton.onClick.AddListener(GoBackToPauseMenu);
        
        // Hide settings panel initially
        settingsPanel.SetActive(false);
    }

    void ToggleSound(bool isOn)
    {
        isSoundOn = isOn;
        AudioListener.volume = isSoundOn ? volumeSlider.value : 0f;

        // Save sound settings
        PlayerPrefs.SetInt("Sound", isSoundOn ? 1 : 0);
        PlayerPrefs.Save();
    }

    void AdjustVolume(float volume)
    {
        if (isSoundOn)
        {
            AudioListener.volume = volume;
        }

        // Save volume settings
        PlayerPrefs.SetFloat("Volume", volume);
        PlayerPrefs.Save();
    }


    void GoBackToPauseMenu()
    {
        settingsPanel.SetActive(false);
    }
}
