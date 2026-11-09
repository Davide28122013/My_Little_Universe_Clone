using UnityEngine;

public class ResourceUI : MonoBehaviour
{
    public const int UnlimitedResourceAmount = int.MaxValue;

    public static ResourceUI Instance;

    public TMPro.TMP_Text woodText;
    public TMPro.TMP_Text stoneText;

    public GameObject woodParticlePrefab; // Particle effect prefab for wood collection
    public GameObject stoneParticlePrefab; // Particle effect prefab for stone collection

    [Tooltip("Keeps the two resources at the maximum supported value and prevents spending.")]
    public bool unlimitedResources = true;

    public int woodCount = UnlimitedResourceAmount;
    public int stoneCount = UnlimitedResourceAmount;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        ApplyUnlimitedResources();
        RefreshResourceText();
    }

    // Update the resource count and trigger particle effects
    public void UpdateResourceCount(string resourceType, int amount)
    {
        if (resourceType == "Wood")
        {
            woodCount = unlimitedResources
                ? UnlimitedResourceAmount
                : ClampResourceChange(woodCount, amount);
            RefreshWoodText();
        }
        else if (resourceType == "Stone")
        {
            stoneCount = unlimitedResources
                ? UnlimitedResourceAmount
                : ClampResourceChange(stoneCount, amount);
            RefreshStoneText();
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
            woodCount = unlimitedResources ? UnlimitedResourceAmount : count;
            RefreshWoodText();
        }
        else if (resourceType == "Stone")
        {
            stoneCount = unlimitedResources ? UnlimitedResourceAmount : count;
            RefreshStoneText();
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

    public void ApplyUnlimitedResources()
    {
        if (!unlimitedResources)
        {
            return;
        }

        woodCount = UnlimitedResourceAmount;
        stoneCount = UnlimitedResourceAmount;
    }

    private static int ClampResourceChange(int currentValue, int change)
    {
        long result = (long)currentValue + change;
        if (result < 0)
        {
            return 0;
        }

        return result > int.MaxValue ? int.MaxValue : (int)result;
    }

    private void RefreshResourceText()
    {
        RefreshWoodText();
        RefreshStoneText();
    }

    private void RefreshWoodText()
    {
        if (woodText != null)
        {
            woodText.text = unlimitedResources ? "Wood: ∞" : "Wood: " + woodCount;
        }
    }

    private void RefreshStoneText()
    {
        if (stoneText != null)
        {
            stoneText.text = unlimitedResources ? "Stone: ∞" : "Stone: " + stoneCount;
        }
    }
}
