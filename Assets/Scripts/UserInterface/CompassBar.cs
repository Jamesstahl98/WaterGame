using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class CompassBar : MonoBehaviour
{
    private RectTransform compassBarTransform;

    [SerializeField] private RectTransform northMarkerTransform;
    [SerializeField] private RectTransform eastMarkerTransform;
    [SerializeField] private RectTransform southMarkerTransform;
    [SerializeField] private RectTransform westMarkerTransform;

    [SerializeField] private Transform cameraTransform;


    private void Start()
    {
        compassBarTransform = GetComponent<RectTransform>();
    }
    private void Update()
    {
        SetMarkerPosition(northMarkerTransform, Vector3.forward * 100000);
        SetMarkerPosition(eastMarkerTransform, Vector3.right * 100000);
        SetMarkerPosition(westMarkerTransform, Vector3.left * 100000);
        SetMarkerPosition(southMarkerTransform, Vector3.back * 100000);
    }

    private void SetMarkerPosition(RectTransform markerTransform, Vector3 worldPosition)
    {
        Vector3 directionToTarget = worldPosition - cameraTransform.position;
        float angle = Vector2.SignedAngle(new Vector2(directionToTarget.x, directionToTarget.z), new Vector2(cameraTransform.forward.x, cameraTransform.forward.z));
        float compassPositionX = Mathf.Clamp(2 * angle / Camera.main.fieldOfView, -1, 1);
        markerTransform.anchoredPosition = new Vector2(compassBarTransform.rect.width / 2 * compassPositionX, 0);
    }
}
