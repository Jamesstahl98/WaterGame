using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPickupable
{
    Sprite GetSprite();
    string GetFishName();
    int GetPrice();
}
