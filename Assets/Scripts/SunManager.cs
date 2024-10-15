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
        lightData.EnableColorTemperature(true);
    }

    public void UpdateSun(float time)
    {
        float intensityCurve = sunIntensityCurve.Evaluate(time / 24f);
        float temperatureCurve = sunTemperatureCurve.Evaluate(time / 24f);

        lightData.SetIntensity(intensityCurve * sunIntensity);
        light.colorTemperature = temperatureCurve * sunTemperature;
    }
}
