using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static DayNightCycle;

public class ClockPointer : MonoBehaviour
{
    [SerializeField] private bool isMinutePointer;
    private DayNightCycle dayNightCycle;
    private RectTransform rectTransform;

    private void Start()
    {
        dayNightCycle = GameObject.Find("TimeManager").GetComponent<DayNightCycle>();
        rectTransform = GetComponent<RectTransform>();
        dayNightCycle.TimeHandlerDelegate += UpdateRotation;
    }

    private void OnDestroy()
    {
        dayNightCycle.TimeHandlerDelegate -= UpdateRotation;
    }

    private void UpdateRotation()
    {
        rectTransform.rotation = Quaternion.Euler(0, 0, isMinutePointer ? -dayNightCycle.CurrentTime * 360 : -dayNightCycle.CurrentTime * 30);
    }
}
