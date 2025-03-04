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
            var item = fishingSpot.fishes[i] as IPickupable;
            if (item.GetUniqueStatus() && PlayerInventory.ItemsInInventory.ContainsKey(item))
            {
                continue;
            }

            for (int j = 0; j < fishingSpot.fishesCount[i]; j++)
            {
                if (fishingSpot.fishesCount[i] < 1)
                {
                    var roll = Random.Range(0, 101) / 100f;
                    Debug.Log(roll);

                    if(roll > fishingSpot.fishesCount[i])
                    {
                        break;
                    }
                }
                var fish = Instantiate(fishPrefab).GetComponent<FishController>();
                fish.Init(fishingSpot.fishes[i] as IPickupable);
            }
        }
    }
}
