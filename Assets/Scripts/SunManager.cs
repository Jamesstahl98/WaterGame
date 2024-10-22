using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;

public class SunManager : MonoBehaviour
{
    private DayNightCycle dayNightCycle;
    private HDAdditionalLightData lightData;
    private Light light;

    [SerializeField] private float sunIntensity;
    [SerializeField] private float sunTemperature;
    [SerializeField] AnimationCurve sunIntensityCurve;
    [SerializeField] AnimationCurve sunTemperatureCurve;

    private void Awake()
    {
        light = GetComponent<Light>();
        lightData = GetComponent<HDAdditionalLightData>();
        dayNightCycle = GameObject.Find("TimeManager").GetComponent<DayNightCycle>();
        dayNightCycle.TimeHandlerDelegate += UpdateSun;
        lightData.EnableColorTemperature(true);
    }

    private void OnDestroy()
    {
        dayNightCycle.TimeHandlerDelegate -= UpdateSun;
    }

    private void UpdateSun()
    {
        float intensityCurve = sunIntensityCurve.Evaluate(dayNightCycle.CurrentTime / 24f);
        float temperatureCurve = sunTemperatureCurve.Evaluate(dayNightCycle.CurrentTime / 24f);

        lightData.SetIntensity(intensityCurve * sunIntensity);
        light.colorTemperature = temperatureCurve * sunTemperature;
    }
}
