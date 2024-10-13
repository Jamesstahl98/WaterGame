using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FishingSpot", menuName = "ScriptableObjects/FishingSpotScriptableObject")]
public class FishingSpotScriptableObject : ScriptableObject
{
    public List<FishScriptableObject> fishes;

}
