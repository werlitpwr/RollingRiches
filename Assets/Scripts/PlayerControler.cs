using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerControler : MonoBehaviour
{
    public float speed = 5;
    public float jumpForce = 10f;
    public LevelInfoManager levelInfo;

    Rigidbody rb;

    float xInput;
    float yInput;
    int lives = 3;
    int score = 0;
    int inventory = 0;
    int winScore = 5;
    bool isGrounded;
    public GameObject TextWin;
    public TextMeshProUGUI LevelText;
    public GameObject GameOverPanel;
    public GameObject WinnerPanel; 
    public TextMeshProUGUI WinnerText; 
    public AudioSource collectCoinSound;
    public AudioSource fallSound; // Add an AudioSource for the falling sound
    public bool hasCollectedCoin = false; 
    public bool hasFallen = false;
    public Button backToMenuButtonGameOver; 
    public Button backToMenuButtonWinner;  
    private Vector3 startingPosition;

    void Start()
    {
        if (PlayerPrefs.HasKey("PlayerPosX") && PlayerPrefs.HasKey("PlayerPosY") && PlayerPrefs.HasKey("PlayerPosZ"))
        {
            float savedX = PlayerPrefs.GetFloat("PlayerPosX");
            float savedY = PlayerPrefs.GetFloat("PlayerPosY");
            float savedZ = PlayerPrefs.GetFloat("PlayerPosZ");

            transform.position = new Vector3(savedX, savedY, savedZ);
            Debug.Log($"Player position restored to: ({savedX}, {savedY}, {savedZ})");
        }
        else
        {
            Debug.Log("No saved position found. Starting at default position.");
        }
        hasFallen = false;
        if (collectCoinSound == null || fallSound == null)
        {
            AudioSource[] audioSources = GetComponents<AudioSource>();
            if (audioSources.Length > 1)
            {
                collectCoinSound = audioSources[0];
                fallSound = audioSources[1];
            }
            else
            {
                Debug.LogError("Not enough AudioSources attached to the GameObject.");
            }
        }
        //WinnerPanel.SetActive(false);
        //GameOverPanel.SetActive(false);
        score = 0;
        inventory = PlayerPrefs.GetInt("PlayerInventory", 0);
        lives = PlayerPrefs.GetInt("PlayerLives", 3);
        winScore = levelInfo.WinScore;  // Access WinScore here
        collectCoinSound = GetComponent<AudioSource>();
        fallSound = GetComponent<AudioSource>() ;

        StartCoroutine(DisplayLevelMessage());
        
        if (backToMenuButtonGameOver != null)
            backToMenuButtonGameOver.onClick.AddListener(BackToMenu);

        if (backToMenuButtonWinner != null)
            backToMenuButtonWinner.onClick.AddListener(BackToMenu);

    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (transform.position.y < -1f && !hasFallen)
        {
            hasFallen = true; // Set the flag to prevent multiple triggers

            // Play the fall sound once
            if (fallSound != null)
            {
                fallSound.Play();
            }
            StartCoroutine(HandleFall());
            
        }
        
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse); 
            isGrounded = false; 
        }
    }

    private void FixedUpdate()
    {
        xInput = Input.GetAxis("Horizontal");
        yInput = Input.GetAxis("Vertical");
        SaveGame();

        rb.AddForce(xInput * speed, 0, yInput * speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Triggered by: " + other.gameObject.name);

        if (other.gameObject.tag == "Coin")
        {
            if (collectCoinSound != null)
            {
                collectCoinSound.Play();
            }
            hasCollectedCoin = true;

            other.gameObject.SetActive(false);
            score++;
            inventory++;

            levelInfo.UpdateScore(score);
            PlayerPrefs.SetInt("PlayerScore", score);
            PlayerPrefs.SetInt("PlayerInventory", inventory);
            PlayerPrefs.Save();

            // Check if the player has won the level (score >= winScore)
            if (score >= winScore)
            {
                if (SceneManager.GetActiveScene().buildIndex == 4) // Check if it's level 3
                {
                    ShowWinnerPanel(); // Show the Winner Panel
                }
                else
                {
                    TextWin.SetActive(true);
                    StartCoroutine(WaitAndLoadNextLevel());
                }
            }
        }
        if (other.gameObject.CompareTag("Ghost"))
        {
            lives--;  // Decrease life when colliding with a ghost
            PlayerPrefs.SetInt("PlayerLives", lives);
            PlayerPrefs.Save();

            DeletePositionPrefs();

            if (lives < 1)
            {
                Debug.Log("Game Over: Lives exhausted");
                GameOverPanel.SetActive(true); // Show the GameOverPanel
                Time.timeScale = 0f;
               
            }
            else
            {
                // Restart the scene if the player loses a life
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }


    private IEnumerator WaitAndLoadNextLevel()
    {
        yield return new WaitForSeconds(2f); // Wait for 2 seconds
        LoadNextLevel(); // Call the function to load the next level
    }

    private IEnumerator DisplayLevelMessage()
    {
        int levelNumber = SceneManager.GetActiveScene().buildIndex - 1 ;
        if (levelNumber != 0)
        {
            LevelText.text = $"LEVEL {levelNumber}";
            LevelText.gameObject.SetActive(true);

            yield return new WaitForSeconds(2f);

            LevelText.gameObject.SetActive(false);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the player collides with the ground
        if (collision.gameObject.CompareTag("GroundLvl1"))
        {
            isGrounded = true; // Allow jumping again
        }
    }

    void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
    }
    public void RestoreSavedData()
    {
        // Restore player stats
        lives = PlayerPrefs.GetInt("PlayerLives", 3);
        score = PlayerPrefs.GetInt("PlayerScore", 0);
        inventory = PlayerPrefs.GetInt("PlayerInventory", 0);

        // Update any UI or game state if necessary
        levelInfo.UpdateScore(score);
    }
    void SaveGame()
    {
        // Save the current level (scene index)
        PlayerPrefs.SetInt("SavedLevel", SceneManager.GetActiveScene().buildIndex);

        // Save the player's lives, score, and inventory
        PlayerPrefs.SetInt("PlayerLives", lives);
        PlayerPrefs.SetInt("PlayerScore", score);
        PlayerPrefs.SetInt("PlayerInventory", inventory);
        // Save player position
        PlayerPrefs.SetFloat("PlayerPosX", transform.position.x);
        PlayerPrefs.SetFloat("PlayerPosY", transform.position.y);
        PlayerPrefs.SetFloat("PlayerPosZ", transform.position.z);

        PlayerPrefs.Save();
        Debug.Log("Game Saved!");
        Debug.Log($"Game Saved! Position: ({transform.position.x}, {transform.position.y}, {transform.position.z})");

    }
    
    private void ShowWinnerPanel()
    {
        Debug.Log("Showing Winner Panel");
        WinnerPanel.SetActive(true); // Show the Winner Panel
        Time.timeScale = 0f;
   
    }
    public void BackToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
    private IEnumerator HandleFall()
    {
        yield return new WaitForSeconds(2f); // Allow the sound to play for a short duration

        lives--;
        score = 0;
        PlayerPrefs.SetInt("PlayerScore", score);
        PlayerPrefs.SetInt("PlayerLives", lives);
        PlayerPrefs.Save();

        DeletePositionPrefs();

        if (lives < 1)
        {
            GameOverPanel.SetActive(true);
            Time.timeScale = 0f; // Pause the game
        }
        else
        {
            hasFallen = false; // Reset the flag before reloading
            transform.position = startingPosition; 
            SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Restart the level
        }
    }
    private void DeletePositionPrefs()
    {
        PlayerPrefs.DeleteKey("PlayerPosX");
        PlayerPrefs.DeleteKey("PlayerPosY");
        PlayerPrefs.DeleteKey("PlayerPosZ");
        PlayerPrefs.Save();
        Debug.Log("Position-related PlayerPrefs deleted.");
    }

}

