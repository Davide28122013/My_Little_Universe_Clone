using UnityEngine;

public class ResourceUI : MonoBehaviour
{
    public const int UnlimitedResourceAmount = int.MaxValue;

    public static ResourceUI Instance;

    public TMPro.TMP_Text woodText;
    public TMPro.TMP_Text stoneText;
    public TMPro.TMP_Text gemsText;
    public TMPro.TMP_Text banknotesText;
    public TMPro.TMP_Text ironText;
    public TMPro.TMP_Text goldText;
    public TMPro.TMP_Text coalText;
    public TMPro.TMP_Text crystalsText;
    public TMPro.TMP_Text swordFragmentsText;

    public GameObject woodParticlePrefab; // Particle effect prefab for wood collection
    public GameObject stoneParticlePrefab; // Particle effect prefab for stone collection

    [Tooltip("Keeps the two resources at the maximum supported value and prevents spending.")]
    public bool unlimitedResources = true;

    public int woodCount = UnlimitedResourceAmount;
    public int stoneCount = UnlimitedResourceAmount;
    public int gemsCount = UnlimitedResourceAmount;
    public int banknotesCount = UnlimitedResourceAmount;
    public int ironCount = UnlimitedResourceAmount;
    public int goldCount = UnlimitedResourceAmount;
    public int coalCount = UnlimitedResourceAmount;
    public int crystalsCount = UnlimitedResourceAmount;
    public int swordFragmentsCount = UnlimitedResourceAmount;

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
        else if (resourceType == "Iron")
        {
            ironCount = unlimitedResources ? UnlimitedResourceAmount : ClampResourceChange(ironCount, amount);
            RefreshIronText();
        }
        else if (resourceType == "Gold")
        {
            goldCount = unlimitedResources ? UnlimitedResourceAmount : ClampResourceChange(goldCount, amount);
            RefreshGoldText();
        }
        else if (resourceType == "Coal")
        {
            coalCount = unlimitedResources ? UnlimitedResourceAmount : ClampResourceChange(coalCount, amount);
            RefreshCoalText();
        }
        else if (resourceType == "Crystals")
        {
            crystalsCount = unlimitedResources ? UnlimitedResourceAmount : ClampResourceChange(crystalsCount, amount);
            RefreshCrystalsText();
        }
        else if (resourceType == "SwordFragments")
        {
            swordFragmentsCount = unlimitedResources ? UnlimitedResourceAmount : ClampResourceChange(swordFragmentsCount, amount);
            RefreshSwordFragmentsText();
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
        else if (resourceType == "Iron")
        {
            ironCount = unlimitedResources ? UnlimitedResourceAmount : count;
            RefreshIronText();
        }
        else if (resourceType == "Gold")
        {
            goldCount = unlimitedResources ? UnlimitedResourceAmount : count;
            RefreshGoldText();
        }
        else if (resourceType == "Coal")
        {
            coalCount = unlimitedResources ? UnlimitedResourceAmount : count;
            RefreshCoalText();
        }
        else if (resourceType == "Crystals")
        {
            crystalsCount = unlimitedResources ? UnlimitedResourceAmount : count;
            RefreshCrystalsText();
        }
        else if (resourceType == "SwordFragments")
        {
            swordFragmentsCount = unlimitedResources ? UnlimitedResourceAmount : count;
            RefreshSwordFragmentsText();
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
        ironCount = UnlimitedResourceAmount;
        goldCount = UnlimitedResourceAmount;
        coalCount = UnlimitedResourceAmount;
        crystalsCount = UnlimitedResourceAmount;
        swordFragmentsCount = UnlimitedResourceAmount;
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
        RefreshIronText();
        RefreshGoldText();
        RefreshCoalText();
        RefreshCrystalsText();
        RefreshSwordFragmentsText();
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

    private void RefreshIronText() => SetText(ironText, "Iron", ironCount);
    private void RefreshGoldText() => SetText(goldText, "Gold", goldCount);
    private void RefreshCoalText() => SetText(coalText, "Coal", coalCount);
    private void RefreshCrystalsText() => SetText(crystalsText, "Crystals", crystalsCount);
    private void RefreshSwordFragmentsText() => SetText(swordFragmentsText, "Sword Fragments", swordFragmentsCount);

    private void SetText(TMPro.TMP_Text label, string labelName, int count)
    {
        if (label != null)
        {
            label.text = unlimitedResources ? labelName + ": ∞" : labelName + ": " + count;
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

        if (ironText == null) ironText = CreateCurrencyLabel("IronText", -210f, new Color(0.7f, 0.7f, 0.75f));
        if (goldText == null) goldText = CreateCurrencyLabel("GoldText", -280f, new Color(1f, 0.8f, 0.1f));
        if (coalText == null) coalText = CreateCurrencyLabel("CoalText", -350f, new Color(0.45f, 0.45f, 0.45f));
        if (crystalsText == null) crystalsText = CreateCurrencyLabel("CrystalsText", -420f, new Color(0.75f, 0.35f, 1f));
        if (swordFragmentsText == null) swordFragmentsText = CreateCurrencyLabel("SwordFragmentsText", -490f, new Color(1f, 0.55f, 0.2f));
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
