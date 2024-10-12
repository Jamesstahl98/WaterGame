using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverAnimation : MonoBehaviour
{
    public float hoverAmplitude = 0.5f;
    public float hoverFrequency = 1f;
    public float hoverOffset = 0f;

    private Vector3 originalPosition;

    void Start()
    {
        originalPosition = transform.position;
    }

    void Update()
    {
        float newY = originalPosition.y + hoverAmplitude * Mathf.Sin(Time.time * hoverFrequency + hoverOffset);

        transform.position = new Vector3(originalPosition.x, newY, originalPosition.z);
    }
}
