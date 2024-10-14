using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FishingSpotOutdoors", menuName = "ScriptableObjects/FishingSpotOutdoorsScriptableObject")]
public class FishingSpotOutdoors : ScriptableObject
{
    public List<FishingSpotScriptableObject> PossibleFishingSpots;
    public Vector3 Position;
    public bool IsFishable;
}
