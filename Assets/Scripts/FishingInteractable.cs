using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FishingInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject userInterfaceGraphic;
    private FishingSpotScriptableObject fishingSpot;
    [SerializeField] private FishingSpotOutdoors fishingSpotOutdoors;
    private bool isActive = true;

    void Awake()
    {
        SetFishingSpot();
    }

    public void SetFishingSpot()
    {
        fishingSpot = fishingSpotOutdoors.PossibleFishingSpots[Random.Range(0, fishingSpotOutdoors.PossibleFishingSpots.Count + 1)];
    }

    public void PlayerEnteredTrigger()
    {
        if(isActive)
            userInterfaceGraphic.SetActive(true);
    }

    public void PlayerLeftTrigger()
    {
        if (isActive)
            userInterfaceGraphic.SetActive(false);
    }

    public void Interact()
    {
        if (isActive)
            SceneSwapper.GoToFishingScene(fishingSpot);
    }
}
