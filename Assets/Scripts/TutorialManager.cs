using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TutorialManager : MonoBehaviour
{
    public GameObject tutorialPanel;   // The panel that holds the tutorial UI elements
    public TextMeshProUGUI tutorialText; // The text component that displays tutorial instructions
    public Button skipButton;           // The button that lets the player skip the tutorial

    private int tutorialStep = 0;       // Tracks the current tutorial step
    private string[] tutorialMessages;  // Array of tutorial messages

    void Start()
    {
        // Define the tutorial messages
        tutorialMessages = new string[]
        {
            "Welcome to the game! Let's get started.",
            "Use the arrow keys or WASD to move.",
            "Press SPACE to jump.",
            "Collect coins to earn points!",
            "Good luck! You can skip this tutorial at any time."
        };

        // Initially hide the tutorial panel
        tutorialPanel.SetActive(true);

        // Set the first tutorial message
        ShowTutorialMessage();

        // Add listener to skip button
        skipButton.onClick.AddListener(SkipTutorial);
    }

    void Update()
    {
        // Wait for player input to proceed to the next step
        if (tutorialStep < tutorialMessages.Length)
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
            {
                tutorialStep++;
                ShowTutorialMessage();
            }
        }
    }

    // Show the current tutorial message
    void ShowTutorialMessage()
    {
        if (tutorialStep < tutorialMessages.Length)
        {
            tutorialText.text = tutorialMessages[tutorialStep];
        }
        else
        {
            EndTutorial();
        }
    }

    // End the tutorial and hide the tutorial panel
    void EndTutorial()
    {
        tutorialPanel.SetActive(false);  // Hide the tutorial panel
        Time.timeScale = 1f;  // Resume the game time (in case it was paused during the tutorial)
    }

    // Skip the tutorial
    void SkipTutorial()
    {
        tutorialStep = tutorialMessages.Length;  // Skip to the end of the tutorial
        ShowTutorialMessage();
    }
}
