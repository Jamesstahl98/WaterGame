using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPickupable
{
    Sprite GetSprite();
    string GetName();
    int GetPrice();
    float GetSpeed();
    float GetMinDepth();
    float GetMaxDepth();
}
