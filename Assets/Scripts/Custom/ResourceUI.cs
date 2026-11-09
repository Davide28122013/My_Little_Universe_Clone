using UnityEngine;

public class ResourceUI : MonoBehaviour
{
    public const int UnlimitedResourceAmount = int.MaxValue;

    public static ResourceUI Instance;

    public TMPro.TMP_Text woodText;
    public TMPro.TMP_Text stoneText;
    public TMPro.TMP_Text gemsText;
    public TMPro.TMP_Text banknotesText;

    public GameObject woodParticlePrefab; // Particle effect prefab for wood collection
    public GameObject stoneParticlePrefab; // Particle effect prefab for stone collection

    [Tooltip("Keeps the two resources at the maximum supported value and prevents spending.")]
    public bool unlimitedResources = true;

    public int woodCount = UnlimitedResourceAmount;
    public int stoneCount = UnlimitedResourceAmount;
    public int gemsCount = UnlimitedResourceAmount;
    public int banknotesCount = UnlimitedResourceAmount;

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
        CreateCurrencyLabels();
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
        else if (resourceType == "Gems")
        {
            gemsCount = unlimitedResources
                ? UnlimitedResourceAmount
                : ClampResourceChange(gemsCount, amount);
            RefreshGemsText();
        }
        else if (resourceType == "Banknotes")
        {
            banknotesCount = unlimitedResources
                ? UnlimitedResourceAmount
                : ClampResourceChange(banknotesCount, amount);
            RefreshBanknotesText();
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
        else if (resourceType == "Gems")
        {
            gemsCount = unlimitedResources ? UnlimitedResourceAmount : count;
            RefreshGemsText();
        }
        else if (resourceType == "Banknotes")
        {
            banknotesCount = unlimitedResources ? UnlimitedResourceAmount : count;
            RefreshBanknotesText();
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

    public int GetGemsCount()
    {
        return gemsCount;
    }

    public int GetBanknotesCount()
    {
        return banknotesCount;
    }

    public void ApplyUnlimitedResources()
    {
        if (!unlimitedResources)
        {
            return;
        }

        woodCount = UnlimitedResourceAmount;
        stoneCount = UnlimitedResourceAmount;
        gemsCount = UnlimitedResourceAmount;
        banknotesCount = UnlimitedResourceAmount;
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
        RefreshGemsText();
        RefreshBanknotesText();
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

    private void RefreshGemsText()
    {
        if (gemsText != null)
        {
            gemsText.text = unlimitedResources ? "Gems: ∞" : "Gems: " + gemsCount;
        }
    }

    private void RefreshBanknotesText()
    {
        if (banknotesText != null)
        {
            banknotesText.text = unlimitedResources ? "Banknotes: ∞" : "Banknotes: " + banknotesCount;
        }
    }

    private void CreateCurrencyLabels()
    {
        if (woodText == null)
        {
            return;
        }

        if (gemsText == null)
        {
            gemsText = CreateCurrencyLabel("GemsText", -70f, new Color(0.2f, 0.9f, 1f));
        }

        if (banknotesText == null)
        {
            banknotesText = CreateCurrencyLabel("BanknotesText", -140f, new Color(0.4f, 1f, 0.45f));
        }
    }

    private TMPro.TMP_Text CreateCurrencyLabel(string labelName, float verticalOffset, Color color)
    {
        TMPro.TMP_Text label = Instantiate(woodText, woodText.transform.parent);
        label.name = labelName;
        label.color = color;

        RectTransform labelTransform = label.rectTransform;
        labelTransform.anchoredPosition += new Vector2(0f, verticalOffset);
        return label;
    }
}
