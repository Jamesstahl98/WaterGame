using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneSwapper
{
    public static FishingSpotScriptableObject FishingSpot;
    //private static GameObject FishingSpotBuilderPrefab;

    public static void GoToFishingScene(FishingSpotScriptableObject fishingSpot)
    {
        FishingSpot = fishingSpot;
        SceneManager.LoadScene("FishingScene");
    }

    public static void GoToBoatScene()
    {

    }
}
