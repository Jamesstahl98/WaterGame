using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Fish", menuName = "ScriptableObjects/FishScriptableObject")]

public class FishScriptableObject : ScriptableObject, IPickupable
{
    public Sprite sprite;

    [Header("Stats")]
    public string fishName;
    public int price;
    public float speed;
    public float minDepth;
    public float maxDepth;

    public Sprite GetSprite() => sprite;
    public string GetFishName() => fishName;
    public int GetPrice() => price;
}
