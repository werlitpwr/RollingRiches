using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelInfoManager : MonoBehaviour
{
    public TextMeshProUGUI userNameText;    // Text element for displaying the username
    public TextMeshProUGUI scoreText;       // Text element for displaying the score
    public TextMeshProUGUI coinsNeededText; // Text element for displaying coins needed
    public TextMeshProUGUI livesText;       // Text element for displaying lives

    private int winScore = 5;  // Coins needed to win
    private int score = 0;      // Player's current score
    private int lives = 3;      // Player's starting lives

    public int WinScore => winScore; // Public property to access winScore

    void Start()
    {
        // Load the user name from PlayerPrefs
        string playerName = PlayerPrefs.GetString("PlayerName", "Player");

        // Display the user's name
        userNameText.text = $"User: {playerName}";

        // Load lives from PlayerPrefs
        lives = PlayerPrefs.GetInt("PlayerLives", 3);
        SetWinScoreForLevel();

        // Initialize the HUD
        UpdateScoreText();
        UpdateCoinsNeededText();
        UpdateLivesText();
    }

    public void UpdateScore(int newScore)
    {
        score = newScore;
        UpdateScoreText();
        UpdateCoinsNeededText();
    }

    public void UpdateLives(int newLives)
    {
        lives = newLives;
        UpdateLivesText();
    }

    private void UpdateScoreText()
    {
        scoreText.text = $"Score: {score}";
    }

    private void UpdateCoinsNeededText()
    {
        int coinsNeeded = Mathf.Max(0, winScore - score);
        coinsNeededText.text = $"Coins Needed: {coinsNeeded}";
    }

    private void UpdateLivesText()
    {
        livesText.text = $"Lives: {lives}";
    }

    private void SetWinScoreForLevel()
    {
        // Check the current scene's name (or build index)
        string currentSceneName = SceneManager.GetActiveScene().name;

        switch (currentSceneName)
        {
            case "Level 1":
                winScore = 2;
                break;
            case "Level 2":
                winScore = 6;
                break;
            case "Level 3":
                winScore = 10;
                break;
            default:
                winScore = 5; // Default value if no specific level is matched
                break;
        }
    }
}
