using System.Collections;
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
    public GameObject ghostObject;
    private int currentStep = 0;
    public GameObject playerBall;

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
        if (currentStep == 5 )
        {
            PositionGhostNearPlayer();
            
        }
        if (currentStep == 6 )
        {
            ghostObject.SetActive(false); 
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
            StartCoroutine(WaitAndEndTutorial());
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
    private void PositionGhostNearPlayer()
    {
        // Calculate a random offset for the ghost's position relative to the player
        float offsetX = 2; // Random horizontal distance (adjust as needed)
        float offsetZ = 2; // Random depth distance (adjust as needed)

        // Randomly choose whether the ghost should be to the left or right of the player
        offsetX *= Random.Range(0, 2) == 0 ? 1 : -1; // Flip the direction randomly

        // Position the ghost
        Vector3 ghostPosition = new Vector3(
            playerBall.transform.position.x + offsetX, 
            playerBall.transform.position.y, 
            playerBall.transform.position.z + offsetZ
        );

        ghostObject.transform.position = ghostPosition; // Set the ghost's position
        ghostObject.SetActive(true);
    }
    private IEnumerator WaitAndEndTutorial()
    {
        yield return new WaitForSeconds(2f); // Wait for 2 seconds
        EndTutorial(); // Call EndTutorial after the wait
    }
}
