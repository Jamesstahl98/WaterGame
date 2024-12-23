using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDescription : MonoBehaviour
{
    private Vector3 target;
    private Vector3 originalPosition;
    [SerializeField] private float duration;

    void Awake()
    {
        originalPosition = gameObject.GetComponent<RectTransform>().localPosition;
        target = GameObject.Find("DescriptionMoveTo").GetComponent<RectTransform>().localPosition;
    }

    public void ShowDescription(bool b)
    {
        if (b)
        {
            //gameObject.GetComponent<RectTransform>().DOLocalMove(target, duration, false).SetEase(Ease.InOutElastic);
        }
        else
        {
            //gameObject.GetComponent<RectTransform>().DOLocalMove(originalPosition, duration, false).SetEase(Ease.InOutElastic);
        }
    }
}

