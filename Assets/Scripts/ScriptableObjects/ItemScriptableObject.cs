using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "ScriptableObjects/ItemScriptableObject")]

public class ItemScriptableObject : ScriptableObject, IPickupable
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
}
