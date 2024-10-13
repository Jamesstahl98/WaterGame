using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FishingInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject userInterfaceGraphic;
    [SerializeField] private FishingSpotScriptableObject fishingSpot;
    [SerializeField] private GameObject fishingSpotBuilderPrefabt;

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
        SceneSwapper.GoToFishingScene(fishingSpot, fishingSpotBuilderPrefabt);
    }
}
