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
                if (fishingSpot.fishesCount[i] < 1)
                {
                    var roll = Random.Range(0, 101) / 100f;

                    if(roll < fishingSpot.fishesCount[i])
                    {
                        continue;
                    }
                }
                var fish = Instantiate(fishPrefab).GetComponent<FishController>();
                fish.Init(fishingSpot.fishes[i] as IPickupable);
            }
        }
    }
}
