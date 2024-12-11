using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class LevelInfoManager : MonoBehaviour
{
    public TextMeshProUGUI userNameText; // Reference to the user name text
    public TextMeshProUGUI scoreText; // Reference to the score text
    public TextMeshProUGUI coinsNeededText; // Reference to the coins needed text

    private int score = 0;
    private int winScore; // This will change per level

    void Start()
    {
        // Load the user name from PlayerPrefs
        string playerName = PlayerPrefs.GetString("PlayerName", "Player");

        // Display the user's name
        userNameText.text = $"User: {playerName}";

        // Set winScore based on the active level
        SetWinScoreForLevel();

        // Initialize the HUD
        UpdateScoreText();
        UpdateCoinsNeededText();
    }

    public void UpdateScore(int newScore)
    {
        score = newScore;
        UpdateScoreText();
        UpdateCoinsNeededText();
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
