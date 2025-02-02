using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MimicPosition : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float division;

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector2(target.position.x / division, transform.position.y);
    }
}
