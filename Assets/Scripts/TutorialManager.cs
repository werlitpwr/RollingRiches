using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class TutorialManager : MonoBehaviour
{
    public TextMeshProUGUI tutorialText; // Reference to the tutorial text
    public Button nextButton;
    public Button backButton;
    public Button skipButton;
    public GameObject panel;
    public PlayerControler playerControler;

    private int currentStep = 0;

    private string[] messages = {
        "Welcome to the game! Meet Ambi, the most ambitious ball you'll ever know.",
        "Ambi dreams of becoming the richest ball in the land! Let's learn how to help him.",
        "Use the arrow keys or WASD to move Ambi around. Try moving now!",
        "Press SPACE to make Ambi jump over obstacles. Give it a try!",
        "Collect coins to earn points! Help Ambi collect the coin in front of you.",
        "Watch out for ghosts! Avoid them to keep Ambi safe.",
        "Be careful not to fall off the plane, or Ambi will lose a life!",
        "Great job! You're ready to help Ambi on his journey. Good luck!"
    };

    
    private void Start()
    {
        panel.SetActive(true);
        tutorialText.text = messages[currentStep];

        nextButton.onClick.AddListener(NextMessage);
        backButton.onClick.AddListener(PreviousMessage);
        skipButton.onClick.AddListener(SkipTutorial);

        UpdateButtonStates();
    }

    private void Update()
    {
        // Check for specific player actions and update the tutorial step
        if (currentStep == 2 && (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D)))
        {
            NextMessage();
        }

        if (currentStep == 3 && Input.GetKeyDown(KeyCode.Space))
        {
            NextMessage();
        }

        if (currentStep == 4 && CheckCoinCollected())
        {
            NextMessage();
        }
    }

    private void NextMessage()
    {
        if (currentStep < messages.Length - 1)
        {
            currentStep++;
            tutorialText.text = messages[currentStep];
        }

        UpdateButtonStates();

        if (currentStep == messages.Length - 1)
        {
            EndTutorial();
        }
    }

    private void PreviousMessage()
    {
        if (currentStep > 0)
        {
            currentStep--;
            tutorialText.text = messages[currentStep];
        }

        UpdateButtonStates();
    }

    private void SkipTutorial()
    {
        EndTutorial();
    }

    private void EndTutorial()
    {
        SceneManager.LoadScene("Level 1");
    }

    private void UpdateButtonStates()
    {
        backButton.interactable = currentStep > 0;
        nextButton.interactable = currentStep < messages.Length - 1;
    }

    private bool CheckCoinCollected()
    {
        // Check if the player has collected a coin
        return playerControler.hasCollectedCoin;
    }
}
