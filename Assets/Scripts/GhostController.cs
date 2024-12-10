using System.Collections; // Dodajemy dla Coroutine
using UnityEngine;

public class GhostController : MonoBehaviour
{
    public Transform player; // Referencja do gracza (kulki)
    public float speed = 3f; // Prędkość ducha
    public float stoppingDistance = 1f; // Minimalna odległość od gracza

    private AudioSource audioSource; // Referencja do komponentu AudioSource

    private void Start()
    {
        // Znajdź komponent AudioSource na obiekcie ducha
        audioSource = GetComponent<AudioSource>();
    }

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
            if (audioSource != null)
            {
                audioSource.Play(); // Odtwarzamy dźwięk natychmiast po kontakcie
            }
            StartCoroutine(WaitForSoundAndReload()); // Czekamy na zakończenie dźwięku
        }
    }

    // Coroutine do załadowania sceny po zakończeniu dźwięku i dodatkowym opóźnieniu
    private IEnumerator WaitForSoundAndReload()
    {
        // Czekamy, aż dźwięk się skończy
        yield return new WaitForSeconds(audioSource.clip.length); // Czekamy na długość dźwięku

        // Dodatkowe 0.5 sekundy opóźnienia po zakończeniu dźwięku
        yield return new WaitForSeconds(0.5f); // Czekamy dodatkowe 0.5 sekundy

        // Załadowanie nowej sceny
        UnityEngine.SceneManagement.SceneManager.LoadScene("Level 1"); // Restartujemy scenę
    }
}
