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
        int savedScene = PlayerPrefs.GetInt("SavedLevel", 1);

        if (PlayerPrefs.HasKey("PlayerPosX") && PlayerPrefs.HasKey("PlayerPosY") && PlayerPrefs.HasKey("PlayerPosZ"))
        {
            SceneManager.sceneLoaded += RestorePlayerPosition; // Attach event to restore position after scene loads
        }

        SceneManager.LoadScene(savedScene);
    }

    private void RestorePlayerPosition(Scene scene, LoadSceneMode mode)
    {
        // Retrieve saved position
        float posX = PlayerPrefs.GetFloat("PlayerPosX", 0f);
        float posY = PlayerPrefs.GetFloat("PlayerPosY", 1f); // Default to a safe height
        float posZ = PlayerPrefs.GetFloat("PlayerPosZ", 0f);

        // Find the PlayerController in the loaded scene
        PlayerControler player = FindObjectOfType<PlayerControler>();
        if (player != null)
        {
            player.transform.position = new Vector3(posX, posY, posZ);
        }

        SceneManager.sceneLoaded -= RestorePlayerPosition; // Detach the event after restoring
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
