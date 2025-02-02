using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPickupable
{
    Sprite GetSprite();
    string GetName();
    string GetDescription();
    int GetSellPrice();
    int GetBuyPrice();
    float GetSpeed();
    float GetMinDepth();
    float GetMaxDepth();
}
