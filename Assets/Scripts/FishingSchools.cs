using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingSchools : MonoBehaviour
{
    List<GameObject> fishingSpotList = new();

    private void Awake()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            fishingSpotList.Add(transform.GetChild(i).gameObject);
            if(fishingSpotList[i].GetComponent<FishingInteractable>().fishingSpotOutdoors.IsFishable == true)
            {
                fishingSpotList[i].SetActive(true);
            }
            else
            {
                fishingSpotList[i].SetActive(false);
            }
        }
    }

    public void EnableFishingSpotsOnDayReset()
    {
        foreach(GameObject item in fishingSpotList)
        {
            item.GetComponent<FishingInteractable>().fishingSpotOutdoors.IsFishable = true;
            item.SetActive(true);
            item.GetComponent<FishingInteractable>().SetFishingSpot();
        }
    }
}
