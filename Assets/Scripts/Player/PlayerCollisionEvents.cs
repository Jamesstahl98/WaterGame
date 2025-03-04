using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionEvents : MonoBehaviour
{
    private IInteractable currentInteractable;

    [SerializeField] private CompassBar compassBar;

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Interactable")
        {
            currentInteractable = other.gameObject.GetComponent<IInteractable>();
            currentInteractable.PlayerEnteredTrigger();
        }
        if(other.tag == "CompassMarkerTrigger")
        {
            compassBar.AddMarker(other.transform.position, other.GetComponent<SpriteRenderer>().sprite);
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.tag == "Interactable")
        {
            other.gameObject.GetComponent<IInteractable>().PlayerLeftTrigger();
            currentInteractable = null;
        }
        if (other.tag == "CompassMarkerTrigger")
        {
            compassBar.RemoveMarker(other.transform.position);
        }
    }

    public void OnTriggerStay(Collider other)
    {
        
    }

    public void Interact()
    {
        if (currentInteractable != null)
        {
            currentInteractable.Interact();
        }
    }
}
