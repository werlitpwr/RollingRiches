using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerControler : MonoBehaviour
{
    public float speed = 5;
    public float jumpForce = 10f;

    Rigidbody rb;

    float xInput;
    float yInput;

    int score = 0;
    bool isGrounded;
    public int winScore = 6;

    public GameObject TextWin;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    

    void Update()
    {
       // Restart the scene if the player falls below a certain height
       if(transform.position.y < -5f) 
       {
            SceneManager.LoadScene("Game");
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

            if(score >= winScore)
            {
                TextWin.SetActive(true);
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
}
