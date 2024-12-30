using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerStats
{
    public delegate void UpgradeDelegate();
    public static UpgradeDelegate UpgradeHandlerDelegate;

    //Boat Stats
    public static float MaxSpeed = 10f;
    public static float MaxThrust = 15f;

    //Fishing Stats
    public static float FishingDepth = 80f;
    public static int FishCount = 1;
}
