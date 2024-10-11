using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;

public class SunManager : MonoBehaviour
{
    private DayNightCycle dayNightCycle;
    private Light light;

    [SerializeField] private float sunIntensity;
    [SerializeField] AnimationCurve sunIntensityCurve;

    private void Awake()
    {
        light = GetComponent<Light>();
        dayNightCycle = GameObject.Find("TimeManager").GetComponent<DayNightCycle>();
    }

    private void Update()
    {
        float intensityCurve = sunIntensityCurve.Evaluate(dayNightCycle.CurrentTime / 24f);
        HDAdditionalLightData lightData = light.GetComponent<HDAdditionalLightData>();

        if(lightData != null)
        {
            light.intensity = intensityCurve * sunIntensity;
        }
    }
}
