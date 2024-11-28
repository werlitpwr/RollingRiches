using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerControler : MonoBehaviour
{
    public float speed = 5;
    public float jumpForce = 10f;
    public int winScore = 6;
    public LevelInfoManager levelInfo;



    Rigidbody rb;

    float xInput;
    float yInput;

    int score = 0;
    int inventory = 0;
    bool isGrounded;
    public GameObject TextWin;

    void Start()
    {
        score = 0;

        inventory = PlayerPrefs.GetInt("PlayerInventory", 0);
    }
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    

    void Update()
    {
       // Restart the scene if the player falls below a certain height
       if(transform.position.y < -5f) 
       {
            SceneManager.LoadScene("Level 1");
       }

       // Check for jump input and if the player is grounded
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse); //up is Shorthand for writing Vector3(0, 1, 0)
            isGrounded = false; // Prevent multiple jumps until landing
        }
    }

    private void FixedUpdate()
    {
        xInput = Input.GetAxis("Horizontal");
        yInput = Input.GetAxis("Vertical");

        rb.AddForce(xInput * speed, 0, yInput * speed);

    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Coin")
        {
            other.gameObject.SetActive(false);
            score++;
            inventory ++;

            levelInfo.UpdateScore(score);
            PlayerPrefs.SetInt("PlayerInventory", inventory);

            if(score >= winScore)
            {
                TextWin.SetActive(true);

                LoadNextLevel();
            }
            

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
}


