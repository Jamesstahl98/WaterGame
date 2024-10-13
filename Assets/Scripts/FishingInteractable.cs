using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FishingInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] private string scene;
    [SerializeField] private GameObject userInterfaceGraphic;

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
        SceneManager.LoadScene(scene);
    }
}
