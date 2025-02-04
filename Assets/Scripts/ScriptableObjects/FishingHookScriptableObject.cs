using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FishingHook", menuName = "ScriptableObjects/FishingHookScriptableObject")]
public class FishingHookScriptableObject : ScriptableObject, IPickupable, IConsumable
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

    [Header("Upgrade stats")]
    public int maxFishesOnHookUpgradeAmount;

    public Sprite GetSprite() => sprite;
    public string GetName() => name;
    public string GetDescription() => description;
    public int GetSellPrice() => sellPrice;
    public int GetBuyPrice() => buyPrice;
    public float GetSpeed() => speed;
    public float GetMinDepth() => minDepth;
    public float GetMaxDepth() => maxDepth;

    public bool Consume()
    {
        if (maxFishesOnHookUpgradeAmount > PlayerStats.FishCount)
        {
            PlayerStats.FishCount = maxFishesOnHookUpgradeAmount;
            PlayerStats.UpgradeHandlerDelegate?.Invoke();
            return true;
        }
        return false;
    }
}