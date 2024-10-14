using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneSwapper
{
    public static FishingSpotScriptableObject FishingSpot;
    public static Dictionary<FishingSpotOutdoors, bool> IsFishingSpotFishable;

    public static void EnableFishingSpots()
    {
        for (int i = 0; i < IsFishingSpotFishable.Count; i++)
        {
            if (IsFishingSpotFishable.ElementAt(i).Value == true)
            {
                IsFishingSpotFishable.ElementAt(i).Key.IsFishable = true;
            }
            else
            {
                IsFishingSpotFishable.ElementAt(i).Key.IsFishable = false;
            }
        }
    }

    public static void GoToFishingScene(FishingSpotScriptableObject fishingSpot)
    {
        FishingSpot = fishingSpot;
        SceneManager.LoadScene("FishingScene");
    }

    public static void GoToBoatScene()
    {
        SceneManager.LoadScene("OutdoorsScene");
    }
}
