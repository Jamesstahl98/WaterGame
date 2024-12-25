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

    public static void GoToFishingScene(FishingSpotScriptableObject fishingSpot)
    {
        GameObject.Find("TimeManager").GetComponent<DayNightCycle>().Stopwatch.Stop();
        UnityEngine.Debug.Log("Stop Time");
        FishingSpot = fishingSpot;
        SceneManager.LoadScene("FishingScene");
    }

    public static void GoToBoatScene()
    {
        SceneManager.LoadScene("OutdoorsScene");
        GameObject.Find("TimeManager").GetComponent<DayNightCycle>().Stopwatch.Start();
        UnityEngine.Debug.Log("Start Time");
    }
}
