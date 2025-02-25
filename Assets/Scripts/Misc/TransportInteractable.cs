using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TransportInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject userInterfaceGraphic;
    [SerializeField] private Transform transportTo;
    [SerializeField] private Transform player;
    public void Interact()
    {
        player.position = transportTo.position;
    }

    public void PlayerEnteredTrigger()
    {
        userInterfaceGraphic.GetComponentInChildren<TextMeshProUGUI>().text = "Press E get transported";
        userInterfaceGraphic.SetActive(true);
    }

    public void PlayerLeftTrigger()
    {
        userInterfaceGraphic.SetActive(false);
    }
}
