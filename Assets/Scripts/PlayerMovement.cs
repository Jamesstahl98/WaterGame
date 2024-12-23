using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    [Header("Thrust")]
    [SerializeField] private float minSpeed;
    [SerializeField] private float minThrust;
    [SerializeField] private float thrustTick;
    [SerializeField] private Slider thrustSlider;

    private float maxThrust;
    private float maxSpeed;

    [Header("Rotation")]
    [SerializeField] private float torqueTick;
    [SerializeField] private float maxTorque;

    private Transform orientation;
    private Rigidbody rb;

    [HideInInspector]public float VerticalInput;
    [HideInInspector]public float HorizontalInput;
    private float currentThrust = 0;
    private float currentTorque = 0;

    void Awake()
    {
        orientation = transform.Find("Orientation");
        rb = GetComponent<Rigidbody>();
        maxThrust = PlayerStats.MaxBoatThrust;
        thrustSlider.maxValue = maxThrust;
        thrustSlider.minValue = minThrust;
        maxSpeed = PlayerStats.MaxBoatSpeed;
    }

    void Update()
    {
        CapSpeed();
    }

    void FixedUpdate()
    {
        MovePlayer();
        RotatePlayer();
    }

    private void MovePlayer()
    {
        if (VerticalInput == 1)
        {
            if (currentThrust < maxThrust)
                currentThrust += thrustTick;
        }
        else if (VerticalInput == -1)
        {
            if (currentThrust > minThrust)
                currentThrust -= thrustTick;
        }

        thrustSlider.value = currentThrust;

        rb.AddForce(transform.forward * currentThrust, ForceMode.Force);
    }

    private void RotatePlayer()
    {
        if (HorizontalInput == 1)
        {
            if (currentTorque < maxTorque)
                currentTorque += torqueTick;
        }
        else if (HorizontalInput == -1)
        {
            if (currentTorque > -maxTorque)
                currentTorque -= torqueTick;
        }

        else
        {
            currentTorque /= 1.03f;
        }

        transform.Rotate(0f, currentTorque, 0f);
    }

    private void CapSpeed()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        if(flatVel.magnitude > maxSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * maxSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }
}
