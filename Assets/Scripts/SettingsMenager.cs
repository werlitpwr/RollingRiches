using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    public GameObject settingsPanel; 
    public Toggle soundToggle;       
    public Button backButton;        
    public Button ballColorButton;  
    private bool isSoundOn = true;

    void Start()
    {
    
        isSoundOn = PlayerPrefs.GetInt("Sound", 1) == 1;
        soundToggle.isOn = isSoundOn; 
        soundToggle.onValueChanged.AddListener(ToggleSound);
        backButton.onClick.AddListener(GoBackToPauseMenu);
        ballColorButton.onClick.AddListener(ChangeBallColor); 
        settingsPanel.SetActive(false);
    }

    void ToggleSound(bool isOn)
    {
        isSoundOn = isOn;
        if (isSoundOn)
        {
           
            AudioListener.volume = 1f; 
        }
        else
        {
        
            AudioListener.volume = 0f; 
        }

    
        PlayerPrefs.SetInt("Sound", isSoundOn ? 1 : 0);
        PlayerPrefs.Save(); 
    }

  
    void GoBackToPauseMenu()
    {
        settingsPanel.SetActive(false); 
       
    }
    void ChangeBallColor()
    {
        // Later, implement functionality to change the ball's color
        Debug.Log("Ball Color Change functionality is not yet implemented.");
    }
}

