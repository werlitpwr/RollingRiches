using UnityEngine;
using TMPro; 
using UnityEngine.SceneManagement;

public class LevelInfoManager : MonoBehaviour
{
    public TextMeshProUGUI userNameText; // Reference to the user name text
    public TextMeshProUGUI scoreText; // Reference to the score text
    public TextMeshProUGUI coinsNeededText; // Reference to the coins needed text

    private int score = 0;
    private int winScore = 6; // Adjust this per level

    void Start()
    {
        // Load the user name from PlayerPrefs
        string playerName = PlayerPrefs.GetString("PlayerName", "Player");

        // Display the user's name
        userNameText.text = $"User: {playerName}";

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
}
