using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FishingInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject userInterfaceGraphic;
    private FishingSpotScriptableObject fishingSpot;
    public FishingSpotOutdoors fishingSpotOutdoors;

    void Awake()
    {
        SetFishingSpot();
    }

    public void SetFishingSpot()
    {
        fishingSpot = fishingSpotOutdoors.PossibleFishingSpots[Random.Range(0, fishingSpotOutdoors.PossibleFishingSpots.Count)];
    }

    public void PlayerEnteredTrigger()
    {
        userInterfaceGraphic.SetActive(true);
    }

    public void PlayerLeftTrigger()
    {
        userInterfaceGraphic.SetActive(false);
    }

    public void Interact()
    {
        fishingSpotOutdoors.IsFishable = false;
        SceneSwapper.GoToFishingScene(fishingSpot);
    }
}
