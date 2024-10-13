using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FishingTrigger : MonoBehaviour
{
    [SerializeField] private Scene scene;

    private bool playerInTrigger = false;
    public void PlayerEnteredTrigger()
    {
        playerInTrigger = true;
    }

    public void PlayerLeftTrigger()
    {
        playerInTrigger = false;
    }

    public void Interact()
    {
        if(playerInTrigger == true)
        {
            SceneManager.LoadScene(scene.ToString());
        }
    }
}
