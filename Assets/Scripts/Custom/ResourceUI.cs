using UnityEngine;

public class ResourceUI : MonoBehaviour
{
    public static ResourceUI Instance;

    public TMPro.TMP_Text woodText;
    public TMPro.TMP_Text stoneText;

    public GameObject woodParticlePrefab; // Particle effect prefab for wood collection
    public GameObject stoneParticlePrefab; // Particle effect prefab for stone collection

    public int woodCount = 1000000000;
    public int stoneCount = 1000000000;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Update the resource count and trigger particle effects
    public void UpdateResourceCount(string resourceType, int amount)
    {
        if (resourceType == "Wood")
        {
            woodCount += amount;
            woodText.text = "Wood: " + woodCount.ToString();
        }
        else if (resourceType == "Stone")
        {
            stoneCount += amount;
            stoneText.text = "Stone: " + stoneCount.ToString();
        }
    }

    // Set the resource count directly
    public void SetResourceCount(string resourceType, int count)
    {
        if (count < 0)
        {
            Debug.LogWarning($"Attempting to set negative resource count: {resourceType} = {count}");
            return; // Prevent setting negative count
        }

        if (resourceType == "Wood")
        {
            woodCount = count; // Assign the loaded count directly
            woodText.text = "Wood: " + woodCount.ToString();
        }
        else if (resourceType == "Stone")
        {
            stoneCount = count; // Assign the loaded count directly
            stoneText.text = "Stone: " + stoneCount.ToString();
        }
    }

    public int GetWoodCount()
    {
        return woodCount;
    }

    public int GetStoneCount()
    {
        return stoneCount;
    }
}
