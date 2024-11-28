using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    public GameObject SettingsPanel;
    public GameObject NameInputPanel; 
    public InputField nameInputField;

    public void Start()
    {
        SettingsPanel.SetActive(false); 
        NameInputPanel.SetActive(false);
    }
    public void OnPlayNewGame()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save(); 
        
        NameInputPanel.SetActive(true);

    }

    public void OnSubmitName()
    {
    string playerName = nameInputField.text;

    if (!string.IsNullOrEmpty(playerName))
    {
        // Clear all previous progress
        PlayerPrefs.DeleteAll();

        // Save the player's name after clearing progress
        PlayerPrefs.SetString("PlayerName", playerName);
        PlayerPrefs.Save(); // Save the changes

        // Load the first level
        SceneManager.LoadScene("Level 1");
    }
    else
    {
        Debug.Log("Player name is empty. Please enter a valid name.");
    }
}


    public void OnPlaySavedGame()
    {
        if (PlayerPrefs.HasKey("PlayerInventory"))
        {
            int savedLevel = PlayerPrefs.GetInt("SavedLevel", 1); 
            SceneManager.LoadScene(savedLevel);
        }
        else
        {
            Debug.Log("No saved game found. Starting a new game.");
            SceneManager.LoadScene("Level 1");
        }
    }
    
    
    public void OnQuitButton ()
    {
        Application.Quit();
    }

    public void OnSettingsButton()
    {
        SettingsPanel.SetActive(true); 
    }

    public void CloseSettings()
    {
        SettingsPanel.SetActive(false); 
    }
}
