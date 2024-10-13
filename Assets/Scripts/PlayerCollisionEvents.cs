using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionEvents : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "FishSchool")
        {
            other.gameObject.GetComponent<FishingTrigger>().PlayerEnteredTrigger();
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.tag == "FishSchool")
        {
            other.gameObject.GetComponent<FishingTrigger>().PlayerLeftTrigger();
        }
    }
}
