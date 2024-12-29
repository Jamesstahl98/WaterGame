using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerInventory
{
    public static Dictionary<IPickupable, int> ItemsInInventory = new Dictionary<IPickupable, int>();
    public static int Money;
}
