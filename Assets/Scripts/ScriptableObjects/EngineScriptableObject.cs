using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Engine", menuName = "ScriptableObjects/EngineScriptableObject")]
public class EngineScriptableObject : ScriptableObject, IPickupable, IConsumable
{
    public Sprite sprite;

    [Header("Stats")]
    public string name;
    public int price;
    public float speed;
    public float minDepth;
    public float maxDepth;

    [Header("Upgrade stats")]
    public float maxSpeedUpgradeAmount;
    public float maxThrustUpgradeAmount;

    public Sprite GetSprite() => sprite;
    public string GetName() => name;
    public int GetPrice() => price;
    public float GetSpeed() => speed;
    public float GetMinDepth() => minDepth;
    public float GetMaxDepth() => maxDepth;

    public bool Consume()
    {
        if(maxSpeedUpgradeAmount > PlayerStats.MaxSpeed)
        {
            PlayerStats.MaxSpeed = maxSpeedUpgradeAmount;
            PlayerStats.MaxThrust = maxThrustUpgradeAmount;
            PlayerStats.UpgradeHandlerDelegate?.Invoke();
            return true;
        }
        return false;
    }
}
