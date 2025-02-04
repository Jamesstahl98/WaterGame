using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrintDepth : MonoBehaviour
{
    [SerializeField] private Transform hookTransform;
    private TMPro.TextMeshProUGUI text;

    private void Start()
    {
        text = GetComponent<TMPro.TextMeshProUGUI>();
    }
    void Update()
    {
        text.text = Math.Round((-hookTransform.position.y)).ToString() + " meters";
    }
}
