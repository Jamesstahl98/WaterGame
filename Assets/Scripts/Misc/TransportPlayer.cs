using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransportPlayer : MonoBehaviour
{
    [SerializeField] private Transform transportTo;
    [SerializeField] private Transform playerTransform;
    public void Transport()
    {
        playerTransform.position = transportTo.position;
    }
}
