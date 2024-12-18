using System.IO;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public void SaveGame(GameState gameState)
    {
        string json = JsonUtility.ToJson(gameState);
        File.WriteAllText(Application.persistentDataPath + "/savegame.json", json);
        Debug.Log("Game Saved");
    }

    public GameState LoadGame()
    {
        string path = Application.persistentDataPath + "/savegame.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            GameState gameState = JsonUtility.FromJson<GameState>(json);
            Debug.Log("Game Loaded");
            return gameState;
        }
        Debug.LogError("Save file not found");
        return null;
    }
}
