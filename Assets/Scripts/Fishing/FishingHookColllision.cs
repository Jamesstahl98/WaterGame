using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingHookColllision : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Console.WriteLine("col");
        if (collision.transform.tag == "Fishable")
        {
            collision.transform.position = transform.position;
            collision.transform.parent = transform;
        }
    }
}
