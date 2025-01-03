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

    void Start()
    {
        SettingsPanel.SetActive(false);
        NameInputPanel.SetActive(false);
    }

    public void OnPlayNewGame()
    {
        PlayerPrefs.DeleteAll(); // Clear all saved data
        PlayerPrefs.Save();

        NameInputPanel.SetActive(true); // Show name input panel
    }

    public void OnSubmitName()
    {
        string playerName = nameInputField.text;

        if (!string.IsNullOrEmpty(playerName))
        {
            // Save the player's name
            PlayerPrefs.SetString("PlayerName", playerName);
            PlayerPrefs.Save();

            // Load the tutorial level (Level 0)
            SceneManager.LoadScene("Level 0");
        }
        else
        {
            Debug.Log("Player name is empty. Please enter a valid name.");
        }
    }

    public void OnPlaySavedGame()
    {
        // Load the saved level or default to Level 1
        int savedScene = PlayerPrefs.GetInt("SavedLevel", 1);

        // Attach the event to restore the player's position
        SceneManager.sceneLoaded += RestorePlayerPosition;

        SceneManager.LoadScene(savedScene);

        Debug.Log("Loading saved game...");
    }

    private void RestorePlayerPosition(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("RestorePlayerPosition called for scene: " + scene.name);

        // Retrieve saved position
        float posX = PlayerPrefs.GetFloat("PlayerPosX", 0f);
        float posY = PlayerPrefs.GetFloat("PlayerPosY", 1f); // Default to a safe height
        float posZ = PlayerPrefs.GetFloat("PlayerPosZ", 0f);

        Debug.Log($"Restoring position to: ({posX}, {posY}, {posZ})");

        // Find the PlayerController in the loaded scene
        PlayerControler player = FindObjectOfType<PlayerControler>();
        if (player != null)
        {
            player.transform.position = new Vector3(posX, posY, posZ);

            Debug.Log("Player position restored successfully.");

            // Optionally restore other saved data if needed
            player.RestoreSavedData();
        }
        else
        {
            Debug.LogError("PlayerControler not found in the scene.");
        }

        // Detach the event to avoid duplicate calls
        SceneManager.sceneLoaded -= RestorePlayerPosition;
    }

    public void OnQuitButton()
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
