using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingSpotOutdoors : ScriptableObject
{
    public List<FishingSpotScriptableObject> PossibleFishingSpots;
    public Vector3 Position;
    public bool IsFishable;
}
