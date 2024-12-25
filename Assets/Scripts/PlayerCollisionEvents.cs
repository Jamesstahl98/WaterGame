using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionEvents : MonoBehaviour
{
    [SerializeField] private IInteractable currentInteractable;

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Interactable")
        {
            other.gameObject.GetComponent<FishingInteractable>().PlayerEnteredTrigger();
            currentInteractable = other.gameObject.GetComponent<FishingInteractable>();
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.tag == "Interactable")
        {
            other.gameObject.GetComponent<FishingInteractable>().PlayerLeftTrigger();
            currentInteractable = null;
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            if(currentInteractable != null)
            {
                currentInteractable.Interact();
            }
        }
    }
}
