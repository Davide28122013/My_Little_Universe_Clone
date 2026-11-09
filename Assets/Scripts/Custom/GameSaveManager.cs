using UnityEngine;
using System.IO;

public class GameSaveManager : MonoBehaviour
{
    public PlayerInventory playerInventory;
    public AreaUnlocker[] areaUnlockers; // All the unlockers in the game

    private string saveFilePath;

    private void Start()
    {
        saveFilePath = Path.Combine(Application.persistentDataPath, "savefile.json");
    }

    // Save game state to JSON
    public void SaveGame()
    {
        GameSaveData saveData = new GameSaveData
        {
            woodCount = playerInventory.woodCount,
            stoneCount = playerInventory.stoneCount,
            areaUnlockers = new AreaUnlockData[areaUnlockers.Length]
        };

        for (int i = 0; i < areaUnlockers.Length; i++)
        {
            saveData.areaUnlockers[i] = areaUnlockers[i].SaveData();
        }

        string json = JsonUtility.ToJson(saveData, true);
        File.WriteAllText(saveFilePath, json);
        Debug.Log("Game saved!");
    }

    // Load game state from JSON
    public void LoadGame()
    {
        if (File.Exists(saveFilePath))
        {
            string json = File.ReadAllText(saveFilePath);
            GameSaveData saveData = JsonUtility.FromJson<GameSaveData>(json);

            playerInventory.woodCount = saveData.woodCount;
            playerInventory.stoneCount = saveData.stoneCount;

            for (int i = 0; i < areaUnlockers.Length; i++)
            {
                areaUnlockers[i].LoadData(saveData.areaUnlockers[i]);
            }

            Debug.Log("Game loaded!");
        }
        else
        {
            Debug.LogWarning("Save file not found!");
        }

        // Saved progress must not disable the clone's unlimited-resources mode.
        if (ResourceUI.Instance != null)
        {
            ResourceUI.Instance.ApplyUnlimitedResources();
            ResourceUI.Instance.SetResourceCount("Wood", ResourceUI.UnlimitedResourceAmount);
            ResourceUI.Instance.SetResourceCount("Stone", ResourceUI.UnlimitedResourceAmount);
        }

        if (playerInventory != null && ResourceUI.Instance != null && ResourceUI.Instance.unlimitedResources)
        {
            playerInventory.woodCount = ResourceUI.UnlimitedResourceAmount;
            playerInventory.stoneCount = ResourceUI.UnlimitedResourceAmount;
        }
    }
}

[System.Serializable]
public class GameSaveData
{
    public int woodCount;
    public int stoneCount;
    public AreaUnlockData[] areaUnlockers;
}
[System.Serializable]
public class AreaUnlockData
{
    public string areaName;
    public int woodProvided;
    public int stoneProvided;
    public bool isUnlocked;
}
