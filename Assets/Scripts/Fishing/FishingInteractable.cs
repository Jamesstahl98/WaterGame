using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class FishingInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject userInterfaceGraphic;
    private FishingSpotScriptableObject fishingSpot;
    public FishingSpotOutdoors fishingSpotOutdoors;

    void Awake()
    {
        fishingSpotOutdoors.Position = transform.position;
        SetFishingSpot();
    }

    public void SetFishingSpot()
    {
        fishingSpot = fishingSpotOutdoors.PossibleFishingSpots[Random.Range(0, fishingSpotOutdoors.PossibleFishingSpots.Count)];
    }

    public void PlayerEnteredTrigger()
    {
        userInterfaceGraphic.GetComponentInChildren<TextMeshProUGUI>().text = "Press E to fish";
        userInterfaceGraphic.SetActive(true);
    }

    public void PlayerLeftTrigger()
    {
        userInterfaceGraphic.SetActive(false);
    }

    public void Interact()
    {
        fishingSpotOutdoors.IsFishable = false;
        PlayerStats.PlayerPosition = transform.position;
        SceneSwapper.GoToFishingScene(fishingSpot);
    }
}
