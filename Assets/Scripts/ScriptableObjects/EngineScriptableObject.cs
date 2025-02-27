using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Engine", menuName = "ScriptableObjects/EngineScriptableObject")]
public class EngineScriptableObject : ScriptableObject, IPickupable, IConsumable
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

    [Header("Upgrade stats")]
    public float maxSpeedUpgradeAmount;
    public float maxThrustUpgradeAmount;

    public Sprite GetSprite() => sprite;
    public string GetName() => name;
    public string GetDescription() => description;
    public int GetSellPrice() => sellPrice;
    public int GetBuyPrice() => buyPrice;
    public float GetSpeed() => speed;
    public float GetMinDepth() => minDepth;
    public float GetMaxDepth() => maxDepth;
    public bool GetSellableStatus() => isSellable;

    public bool Consume()
    {
        if(maxSpeedUpgradeAmount > PlayerStats.MaxSpeed)
        {
            PlayerStats.EngineTier = name;
            PlayerStats.MaxSpeed = maxSpeedUpgradeAmount;
            PlayerStats.MaxThrust = maxThrustUpgradeAmount;
            PlayerStats.UpgradeHandlerDelegate?.Invoke();
            return true;
        }
        return false;
    }
}
