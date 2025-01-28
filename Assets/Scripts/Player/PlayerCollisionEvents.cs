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
            currentInteractable = other.gameObject.GetComponent<IInteractable>();
            currentInteractable.PlayerEnteredTrigger();
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.tag == "Interactable")
        {
            other.gameObject.GetComponent<IInteractable>().PlayerLeftTrigger();
            currentInteractable = null;
        }
    }

    public void Interact()
    {
        if (currentInteractable != null)
        {
            currentInteractable.Interact();
        }
    }
}
