using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class GameState
{
    public string sceneName;
    public float playerPosX, playerPosY, playerPosZ;
    public float playerRotX, playerRotY, playerRotZ;
    public int playerHealth;
    public List<string> inventoryItems = new List<string>();
    public Dictionary<string, bool> objectStates = new Dictionary<string, bool>();
}
