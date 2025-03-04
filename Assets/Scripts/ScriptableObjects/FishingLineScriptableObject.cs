using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FishingLine", menuName = "ScriptableObjects/FishingLineScriptableObject")]
public class FishingLineScriptableObject : ScriptableObject, IPickupable, IConsumable
{
    public Sprite sprite;

    [Header("Stats")]
    public string name;
    public string description;
    public int sellPrice;
    public int buyPrice;
    public float speed;
    public float minDepth;
    public float maxDepth;
    public bool isSellable;
    public bool isUnique;

    [Header("Upgrade stats")]
    public float maxDepthUpgradeAmount;

    public Sprite GetSprite() => sprite;
    public string GetName() => name;
    public string GetDescription() => description;
    public int GetSellPrice() => sellPrice;
    public int GetBuyPrice() => buyPrice;
    public float GetSpeed() => speed;
    public float GetMinDepth() => minDepth;
    public float GetMaxDepth() => maxDepth;
    public bool GetSellableStatus() => isSellable;
    public bool GetUniqueStatus() => isUnique;

    public bool Consume()
    {
        if (maxDepthUpgradeAmount > PlayerStats.MaxSpeed)
        {
            PlayerStats.FishingLineTier = name;
            PlayerStats.FishingDepth = maxDepthUpgradeAmount;
            PlayerStats.UpgradeHandlerDelegate?.Invoke();
            return true;
        }
        return false;
    }
}