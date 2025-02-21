using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingHookColllision : MonoBehaviour
{
    private int catchesLeft = PlayerStats.FishCount;
    private List<IPickupable> caughtObjects = new List<IPickupable>();

    [SerializeField] private Transform fishParent;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Fishable" && catchesLeft > 0)
        {
            if (!enabled) return;
            collision.transform.position = fishParent.position;
            collision.transform.parent = fishParent;
            collision.GetComponent<FishController>().HookCollision();
            caughtObjects.Add(collision.GetComponent<FishController>().ScriptableObject);
            catchesLeft--;
        }
        if(collision.transform.tag == "SceneChanger")
        {
            foreach(IPickupable item in caughtObjects)
            {
                if(PlayerInventory.ItemsInInventory.ContainsKey(item))
                {
                    PlayerInventory.ItemsInInventory[item]++;
                }
                else
                {
                    PlayerInventory.ItemsInInventory.Add(item, 1);
                }
            }
            SceneSwapper.GoToBoatScene();
        }
    }
}
