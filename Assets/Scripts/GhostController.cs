using System.Collections; // Dodajemy dla Coroutine
using UnityEngine;

public class GhostController : MonoBehaviour
{
    public Transform player; // Referencja do gracza (kulki)
    public float speed = 3f; // Prędkość ducha
    public float stoppingDistance = 1f; // Minimalna odległość od gracza

    private void Update()
    {
        // Oblicz odległość między duchem a graczem
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // Jeśli odległość jest większa niż stoppingDistance, poruszaj się w stronę gracza
        if (distanceToPlayer > stoppingDistance)
        {
            Vector3 direction = (player.position - transform.position).normalized;
            transform.position += direction * speed * Time.deltaTime;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Jeśli duch zderzy się z graczem
        if (other.CompareTag("Player"))
        {
            Debug.Log("Game Over");
            StartCoroutine(LoadSceneWithDelay(1f)); // Uruchamiamy Coroutine z opóźnieniem 1 sekundy
        }
    }

    // Coroutine do załadowania sceny po opóźnieniu
    private IEnumerator LoadSceneWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay); // Czekamy określony czas (1 sekunda)
        UnityEngine.SceneManagement.SceneManager.LoadScene("Level 1"); // Restartujemy scenę
    }
}
