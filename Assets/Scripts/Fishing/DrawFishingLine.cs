using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawFishingLine : MonoBehaviour
{
    private LineRenderer lineRenderer;

    [SerializeField] private Transform[] linePoints;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = linePoints.Length;

        for (int i = 0; i < linePoints.Length; i++)
        {
            lineRenderer.SetPosition(i, linePoints[i].position);
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < linePoints.Length; i++)
        {
            lineRenderer.SetPosition(i, linePoints[i].position);
        }
    }
}
