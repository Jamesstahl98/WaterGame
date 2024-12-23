using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ItemData : ScriptableObject
{
    [Header("Dimensions")]
    public int width = 1;
    public int height = 1;

    [Header("Description")]
    public GameObject descriptionObject;
    public string itemName;
    public string descriptionText;
    public string flavorText;

    [Header("Misc")]
    public Sprite itemSprite;

    [Header("Rarity goes from 0 to 5 (0 is the least rare)")]
    public int rarity;
}
