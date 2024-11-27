using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    public GameObject SettingsPanel;

    public void Start()
    {
        SettingsPanel.SetActive(false); 
    }
    public void OnPlayButton ()
    {
        SceneManager.LoadScene(1);
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
