using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    [SerializeField] private float startTime = 8f;
    public float CurrentTime
    {
        get { return (Time.time + startTime) % 24f; }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.K))
        {
            Debug.Log(CurrentTime);
        }
    }
}
