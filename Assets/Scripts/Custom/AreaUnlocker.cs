using UnityEngine;
using UnityEngine.UI;

public class AreaUnlocker : MonoBehaviour
{
    [Tooltip("Unlocks this area immediately when the game starts.")]
    public bool unlockAtStart = false;
    public string areaName;
    public int woodRequired;
    public int stoneRequired;

    private int woodProvided = 0;  // Tracks how much wood has been provided
    private int stoneProvided = 0; // Tracks how much stone has been provided

    public bool isUnlocked = false;

    // Reference to Resource UI or Resource Manager (using Singleton)
    private ResourceUI resourceUI;

    // References to visual and physical barriers of the locked area
    public GameObject lockedVisual;  // e.g., a fence or door

    // Reference to the world canvas with progress bar and text
    public Canvas worldCanvas;
    public Image progressBar; // Image used as the fillable progress bar
    public TMPro.TextMeshProUGUI progressText; // Text to display current progress

    private void Start()
    {
        resourceUI = ResourceUI.Instance; // Access the ResourceUI singleton

        if (unlockAtStart)
        {
            isUnlocked = true;
            woodProvided = woodRequired;
            stoneProvided = stoneRequired;
        }

        UpdateAreaState(); // Ensure the area reflects the correct state (locked/unlocked) on start

        // Initialize the progress bar and text
        UpdateProgressUI();
    }

    public void TryUnlockArea()
    {
        if (isUnlocked)
        {
            Debug.Log($"{areaName} is already unlocked!");
            return;
        }

        // Calculate the remaining required resources
        int woodRemaining = woodRequired - woodProvided;
        int stoneRemaining = stoneRequired - stoneProvided;

        // Get the current resources the player has
        int playerWood = resourceUI.GetWoodCount();
        int playerStone = resourceUI.GetStoneCount();

        // Determine how much of the required resources the player can provide
        int woodToSubmit = Mathf.Min(playerWood, woodRemaining);
        int stoneToSubmit = Mathf.Min(playerStone, stoneRemaining);

        // Add the provided resources to the totals
        woodProvided += woodToSubmit;
        stoneProvided += stoneToSubmit;

        // Unlimited mode deliberately leaves the displayed resource amount unchanged.
        if (resourceUI != null)
        {
            resourceUI.UpdateResourceCount("Wood", -woodToSubmit);
            resourceUI.UpdateResourceCount("Stone", -stoneToSubmit);
        }

        Debug.Log($"Submitted {woodToSubmit} wood and {stoneToSubmit} stone to {areaName}");

        // Update the progress UI
        UpdateProgressUI();

        // Check if the area is now fully unlocked
        if (woodProvided >= woodRequired && stoneProvided >= stoneRequired)
        {
            UnlockArea();
        }
        else
        {
            Debug.Log($"{areaName} is not fully unlocked yet. Wood needed: {woodRequired - woodProvided}, Stone needed: {stoneRequired - stoneProvided}");
        }
    }

    public void UnlockArea(bool deductResources = true)
    {
        isUnlocked = true;

        if (lockedVisual != null)
        {
            lockedVisual.SetActive(false);
        }

        Collider areaCollider = GetComponent<Collider>();
        if (areaCollider != null)
        {
            areaCollider.enabled = false;
        }

        // Disable the world canvas since the area is unlocked
        if (worldCanvas != null)
        {
            worldCanvas.gameObject.SetActive(false);
        }

        Debug.Log($"{areaName} has been unlocked!");
    }

    private void UpdateAreaState()
    {
        if (isUnlocked)
        {
            if (lockedVisual != null)
            {
                lockedVisual.SetActive(false);
            }

            Collider areaCollider = GetComponent<Collider>();
            if (areaCollider != null)
            {
                areaCollider.enabled = false;
            }

            if (worldCanvas != null)
            {
                worldCanvas.gameObject.SetActive(false); // Hide the world canvas if unlocked
            }
        }
        else
        {
            if (lockedVisual != null)
            {
                lockedVisual.SetActive(true);
            }

            if (worldCanvas != null)
            {
                worldCanvas.gameObject.SetActive(true);  // Show the world canvas if not yet unlocked
            }

            // Ensure the progress bar and text are updated initially
            UpdateProgressUI();
        }
    }

    // Update the progress UI elements: progress bar and optional text
    private void UpdateProgressUI()
    {
        // Calculate total progress as a percentage
        float totalRequired = woodRequired + stoneRequired;
        float totalProvided = woodProvided + stoneProvided;
        float progress = totalProvided / totalRequired;

        // Update the progress bar fill amount
        if (progressBar != null)
        {
            progressBar.fillAmount = progress; // Fill amount should be between 0 and 1
        }

        // Update the progress text (optional)
        if (progressText != null)
        {
            progressText.text = $"Wood: {woodProvided}/{woodRequired} \nStone: {stoneProvided}/{stoneRequired}";
        }
    }

    // Save and Load functions to track the progress
    public AreaUnlockData SaveData()
    {
        return new AreaUnlockData
        {
            areaName = areaName,
            woodProvided = woodProvided,
            stoneProvided = stoneProvided,
            isUnlocked = isUnlocked
        };
    }

    public void LoadData(AreaUnlockData data)
    {
        if (unlockAtStart)
        {
            woodProvided = woodRequired;
            stoneProvided = stoneRequired;
            isUnlocked = true;
            UpdateAreaState();
            return;
        }

        woodProvided = data.woodProvided;
        stoneProvided = data.stoneProvided;
        isUnlocked = data.isUnlocked;

        // Update the area state based on the loaded data
        UpdateAreaState();
    }
}
