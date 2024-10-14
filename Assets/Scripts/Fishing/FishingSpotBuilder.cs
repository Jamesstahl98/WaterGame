using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FishingSpotBuilder : MonoBehaviour
{
    [SerializeField] private GameObject fishPrefab;

    private void Awake()
    {
        BuildFishingSpot(SceneSwapper.FishingSpot);
    }

    public void BuildFishingSpot(FishingSpotScriptableObject fishingSpot)
    {
        for (int i = 0; i < fishingSpot.fishes.Count; i++)
        {
            for (int j = 0; j < fishingSpot.fishesCount[i]; j++)
            {
                var fish = Instantiate(fishPrefab).GetComponent<FishController>();
                fish.Init(fishingSpot.fishes[i]);
            }
        }
    }
}
