using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using UnityEngine;
using UnityEngine.UI;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class CompassBar : MonoBehaviour
{
    private RectTransform compassBarTransform;

    [SerializeField] private RectTransform northMarkerTransform;
    [SerializeField] private RectTransform eastMarkerTransform;
    [SerializeField] private RectTransform southMarkerTransform;
    [SerializeField] private RectTransform westMarkerTransform;
    [SerializeField] GameObject markerObject;

    [SerializeField] private Transform cameraTransform;

    private Dictionary<RectTransform, Vector3> nearbyMarkers = new Dictionary<RectTransform, Vector3>();
    private Dictionary<GameObject, Vector3> addedMarkerObjects = new Dictionary<GameObject, Vector3>();

    private void Start()
    {
        compassBarTransform = GetComponent<RectTransform>();
    }
    private void Update()
    {
        UpdateMarkerPositions();
    }

    private void SetMarkerPosition(RectTransform markerTransform, Vector3 worldPosition)
    {
        Vector3 directionToTarget = worldPosition - cameraTransform.position;
        float angle = Vector2.SignedAngle(new Vector2(directionToTarget.x, directionToTarget.z), new Vector2(cameraTransform.forward.x, cameraTransform.forward.z));
        float compassPositionX = Mathf.Clamp(2 * angle / Camera.main.fieldOfView, -1, 1);
        markerTransform.anchoredPosition = new Vector2(compassBarTransform.rect.width / 2 * compassPositionX, 0);
    }

    public void AddMarker(Vector3 position, Sprite sprite)
    {
        var icon = Instantiate(markerObject, transform);
        icon.GetComponent<Image>().sprite = sprite;
        nearbyMarkers.Add(icon.GetComponent<RectTransform>(), position);
        addedMarkerObjects.Add(icon, position);
    }

    public void RemoveMarker(Vector3 position)
    {
        nearbyMarkers.Remove(nearbyMarkers.Where(m => m.Value == position).FirstOrDefault().Key);
        var objectToRemove = addedMarkerObjects.Where(m => m.Value == position).FirstOrDefault().Key;
        Destroy(objectToRemove);
        addedMarkerObjects.Remove(objectToRemove);
    }

    private void UpdateMarkerPositions()
    {
        //Cardinal directions
        SetMarkerPosition(northMarkerTransform, Vector3.forward * 100000);
        SetMarkerPosition(eastMarkerTransform, Vector3.right * 100000);
        SetMarkerPosition(westMarkerTransform, Vector3.left * 100000);
        SetMarkerPosition(southMarkerTransform, Vector3.back * 100000);

        //Quests
        foreach(var item in nearbyMarkers)
        {
            SetMarkerPosition(item.Key, item.Value);
        }
    }
}
