using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPickupable
{
    Sprite GetSprite();
    string GetName();
    int GetSellPrice();
    int GetBuyPrice();
    float GetSpeed();
    float GetMinDepth();
    float GetMaxDepth();
}
