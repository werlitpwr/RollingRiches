using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

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
    public AudioSource collectSound;
    public bool hasCollectedCoin = false;  // Flag to track if the player collected a coin


    void Start()
    {
        score = 0;
        inventory = PlayerPrefs.GetInt("PlayerInventory", 0);
        lives = PlayerPrefs.GetInt("PlayerLives", 3);

        // Get winScore from LevelInfoManager
        winScore = levelInfo.WinScore;  // Access WinScore here

        StartCoroutine(DisplayLevelMessage());
        collectSound = GetComponent<AudioSource>();
        GameOverPanel.SetActive(false);
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Restart the scene if the player falls below a certain height
        if (transform.position.y < -5f)
        {
            lives--;
            score = 0;
            PlayerPrefs.SetInt("PlayerScore", score);
            PlayerPrefs.SetInt("PlayerLives", lives);
            PlayerPrefs.Save();

            if (lives < 1)
            {
                GameOverPanel.SetActive(true); // Show the GameOverPanel
                Time.timeScale = 0f; // Pause the game
            }
            else
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }

        // Check for jump input and if the player is grounded
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse); //up is shorthand for writing Vector3(0, 1, 0)
            isGrounded = false; // Prevent multiple jumps until landing
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
            if (collectSound != null)
            {
                collectSound.Play();
            }
            hasCollectedCoin = true;

            other.gameObject.SetActive(false);
            score++;
            inventory++;

            levelInfo.UpdateScore(score);
            PlayerPrefs.SetInt("PlayerScore", score);
            PlayerPrefs.SetInt("PlayerInventory", inventory);
            PlayerPrefs.Save();
            if (score >= winScore)
            {
                TextWin.SetActive(true);
                StartCoroutine(WaitAndLoadNextLevel());
            }
        }
        if (other.gameObject.CompareTag("Ghost"))
        {
            lives--;  // Decrease life when colliding with a ghost
            PlayerPrefs.SetInt("PlayerLives", lives);
            PlayerPrefs.Save();

            if (lives < 1)
            {
                GameOverPanel.SetActive(true); // Show the GameOverPanel
                Time.timeScale = 0f; // Pause the game
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
        int levelNumber = SceneManager.GetActiveScene().buildIndex;
        if (levelNumber != 0 )
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
    }
}
