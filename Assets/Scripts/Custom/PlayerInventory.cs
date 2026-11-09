using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class PlayerInventory
{
    public int woodCount = 0;
    public int stoneCount = 0;
    public int gemsCount = 0;
    public int banknotesCount = 0;

    // Deduct resources from the player's inventory
    public void DeductResources(ref int woodNeeded, ref int stoneNeeded)
    {
        int woodToDeduct = Mathf.Min(woodCount, woodNeeded);
        woodCount -= woodToDeduct;
        woodNeeded -= woodToDeduct;

        int stoneToDeduct = Mathf.Min(stoneCount, stoneNeeded);
        stoneCount -= stoneToDeduct;
        stoneNeeded -= stoneToDeduct;
    }
}
