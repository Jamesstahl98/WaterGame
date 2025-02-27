using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerStats
{
    public delegate void UpgradeDelegate();
    public static UpgradeDelegate UpgradeHandlerDelegate;

    //Boat Stats
    public static string EngineTier = "Tier One Engine";
    public static float MaxSpeed = 10f;
    public static float MaxThrust = 15f;

    //Fishing Line Stats
    public static string FishingLineTier = "Tier One Fishing Line";
    public static float FishingDepth = 80f;

    //Fishing Hook Stats
    public static string FishingHookTier = "Tier One Fishing Hook";
    public static int FishCount = 1;

    public static Vector3 PlayerPosition = new Vector3(0, 0, -69.8f);
}
