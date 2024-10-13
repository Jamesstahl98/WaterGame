using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneSwapper
{
    private static FishingSpotScriptableObject FishingSpot;
    private static GameObject FishingSpotBuilderPrefab;

    public static void GoToFishingScene(FishingSpotScriptableObject fishingSpot, GameObject fishingSpotBuilder)
    {
        FishingSpot = fishingSpot;
        FishingSpotBuilderPrefab = fishingSpotBuilder;
        LoadSceneAsync();
    }

    private static void LoadSceneAsync()
    {
        CoroutineRunner.StartRoutine(LoadSceneCoroutine());
    }

    private static IEnumerator LoadSceneCoroutine()
    {
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync("FishingScene");

        while (!asyncOperation.isDone)
        {
            yield return null;
        }

        ExecuteAfterSceneLoaded();
    }

    private static void ExecuteAfterSceneLoaded()
    {
        var fishingSpotBuilderObject = GameObject.Instantiate(FishingSpotBuilderPrefab);
        fishingSpotBuilderObject.GetComponent<FishingSpotBuilder>().BuildFishingSpot(FishingSpot);
    }

    public static void GoToBoatScene()
    {

    }
}
