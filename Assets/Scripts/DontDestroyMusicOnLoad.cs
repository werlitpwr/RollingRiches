using UnityEngine;

public class DontDestroyMusicOnLoad : MonoBehaviour
{
    private static DontDestroyMusicOnLoad instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Unity func which allows to save object between different scenes
        }
        else
        {
            Destroy(gameObject); // avoid duplicats
        }
    }
}
