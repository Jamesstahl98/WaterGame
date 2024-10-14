using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockPointer : MonoBehaviour
{
    [SerializeField] private bool isMinutePointer;
    private DayNightCycle dayNightCycle;
    private RectTransform rectTransform;

    private void Awake()
    {
        dayNightCycle = GameObject.Find("TimeManager").GetComponent<DayNightCycle>();
        rectTransform = GetComponent<RectTransform>();
    }

    private void Update()
    {
        UpdateRotation();
    }

    private void UpdateRotation()
    {
        rectTransform.rotation = Quaternion.Euler(0, 0, isMinutePointer ? -dayNightCycle.CurrentTime * 360 : -dayNightCycle.CurrentTime * 15);
    }
}
