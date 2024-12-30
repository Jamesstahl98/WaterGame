using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FishingSpot", menuName = "ScriptableObjects/FishingSpotScriptableObject")]
public class FishingSpotScriptableObject : ScriptableObject
{
    [Header("Fishes and FishesCount must have the same number of elements")]
    public List<ScriptableObject> fishes;
    public List<int> fishesCount;
}