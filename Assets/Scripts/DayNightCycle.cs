using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    [SerializeField] private float timeModifier = 1f;
    [SerializeField] private float startTime = 8f;
    [SerializeField] private SunManager sunManager;
    [SerializeField] private FishingSchools fishingSchoolsParent;

    private bool isEvening;

    [HideInInspector] public Stopwatch Stopwatch = new Stopwatch();

    public float CurrentTime
    {
        get { return ((Stopwatch.ElapsedMilliseconds / 1000f * timeModifier) + startTime) % 24f; }
    }

    private void Awake()
    {
        Stopwatch.Start();
    }

    private void Update()
    {
        if(sunManager != null)
        {
            sunManager.UpdateSun(CurrentTime);
        }
        if (CurrentTime >= 20)
        {
            isEvening = true;
        }
        if(CurrentTime < 20 && isEvening)
        {
            GameObject.Find("FishingSchools").GetComponent<FishingSchools>().EnableFishingSpotsOnDayReset();
            isEvening = false;
        }
    }
}
