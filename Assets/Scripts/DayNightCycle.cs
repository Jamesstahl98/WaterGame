using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DayNightCycle : MonoBehaviour
{
    [SerializeField] private float timeModifier = 1f;
    [SerializeField] private float startTime = 8f;
    [SerializeField] private FishingSchools fishingSchoolsParent;

    private bool isEvening;

    public delegate void TimeDelegate();
    public TimeDelegate TimeHandlerDelegate;

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
        TimeHandlerDelegate?.Invoke();

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
