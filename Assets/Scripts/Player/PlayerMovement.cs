using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    [Header("Thrust")]
    private float maxSpeed;
    private float maxThrust;
    [SerializeField] private float minSpeed;
    [SerializeField] private float minThrust;
    [SerializeField] private float thrustTick;
    [SerializeField] private Slider thrustSlider;
    [SerializeField] private TextMeshProUGUI speedText;

    [Header("Rotation")]
    [SerializeField] private float torqueTick;
    [SerializeField] private float maxTorque;

    private Rigidbody rb;

    public float VerticalInput;
    public float HorizontalInput;
    private float currentThrust = 0;
    private float currentTorque = 0;

    private bool stopMovement;

    public delegate void TransportPlayerDelegate(Vector3 position);
    public static TransportPlayerDelegate TransportPlayerHandlerDelegate;

    void Awake()
    {
        TransportPlayerHandlerDelegate += TransportPlayer;
        PlayerStats.UpgradeHandlerDelegate += UpdateStats;
        UpdateStats();
        rb = GetComponent<Rigidbody>();
        thrustSlider.minValue = minThrust;
        transform.position = PlayerStats.PlayerPosition;
        Physics.SyncTransforms();
    }
    private void TransportPlayer(Vector3 position)
    {
        transform.position = position;
    }
    void Update()
    {
        CapSpeed();
        PrintSpeed();
    }

    private void PrintSpeed()
    {
        speedText.text = $"{GetSpeed().ToString("N0")} km/h";
    }
    private float GetSpeed()
    {
        return rb.velocity.magnitude * 5;
    }

    void FixedUpdate()
    {
        MovePlayer();
        RotatePlayer();
    }

    public void StopMovement(bool b)
    {
        stopMovement = b;
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

    public void UpdateStats()
    {
        maxSpeed = PlayerStats.MaxSpeed;
        maxThrust = PlayerStats.MaxThrust;
        thrustSlider.maxValue = maxThrust;
    }
}
