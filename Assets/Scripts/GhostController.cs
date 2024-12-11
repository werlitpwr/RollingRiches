using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GhostController : MonoBehaviour
{
    public Transform player; // ball ref
    public float speed;
    public float speed3lvl = 4f; 
    public float speed2lvl = 2f; 
    public float stoppingDistance = 1f; //min player - ghost distance

    private AudioSource audioSource;

    private void Start()
    {
        // find on ghost object
        audioSource = GetComponent<AudioSource>();
        Scene currentScene = SceneManager.GetActiveScene();
    }

    private void Update()
    {   
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer > stoppingDistance)
        {
            Scene currentScene = SceneManager.GetActiveScene();

            if (currentScene.name == "Level 2")
            {
                speed = speed2lvl;
            } 
            else if (currentScene.name == "Level 3")
            {
                speed = speed3lvl;
            }

            // move in the ball direction
            Vector3 direction = (player.position - transform.position).normalized;
            transform.position += direction * speed * Time.deltaTime;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // collision shost - ball
        if (other.CompareTag("Player"))
        {
            Debug.Log("Game Over");
            if (audioSource != null)
            {
                audioSource.Play(); 
            }
            StartCoroutine(WaitForSoundAndReload()); // wait for the sound end
        }
    }

    // Coroutine for load page after ending of the sound
    private IEnumerator WaitForSoundAndReload()
    {
        yield return new WaitForSeconds(audioSource.clip.length); 

        yield return new WaitForSeconds(0.5f); 

        // Załadowanie nowej sceny
        SceneManager.LoadScene("Level 1");
    }
}
