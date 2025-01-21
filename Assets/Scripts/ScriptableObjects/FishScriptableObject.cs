using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Fish", menuName = "ScriptableObjects/FishScriptableObject")]

public class FishScriptableObject : ScriptableObject, IPickupable
{
    public Sprite sprite;

    [Header("Stats")]
    public string name;
    public int sellPrice;
    public int buyPrice;
    public float speed;
    public float minDepth;
    public float maxDepth;

    public Sprite GetSprite() => sprite;
    public string GetName() => name;
    public int GetSellPrice() => sellPrice;
    public int GetBuyPrice() => buyPrice;
    public float GetSpeed() => speed;
    public float GetMinDepth() => minDepth;
    public float GetMaxDepth() => maxDepth;
}
