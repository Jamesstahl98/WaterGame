using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float maxSpeed;
    [SerializeField] private float minSpeed;
    [SerializeField] private float maxThrust;
    [SerializeField] private float minThrust;
    [SerializeField] private float gasTick;
    [SerializeField] private float rotateSpeed;

    [SerializeField] private Slider thrustSlider;

    private Transform orientation;
    private Rigidbody rb;

    private float verticalInput;
    private float horizontalInput;
    private float currentThrust = 0;

    void Awake()
    {
        orientation = transform.Find("Orientation");
        rb = GetComponent<Rigidbody>();
        thrustSlider.maxValue = maxThrust;
        thrustSlider.minValue = minThrust;
    }

    void Update()
    {
        MyInput();
        SpeedControl();
    }

    void FixedUpdate()
    {
        MovePlayer();
    }

    private void MyInput()
    {
        verticalInput = Input.GetAxisRaw("Vertical");
        horizontalInput = Input.GetAxisRaw("Horizontal");

        if (verticalInput == 1)
        {
            if (currentThrust > maxThrust) { return; }
            currentThrust += gasTick;
        }
        else if (verticalInput == -1)
        {
            if (currentThrust < minThrust) { return; }
            currentThrust -= gasTick;
        }
        thrustSlider.value = currentThrust;
    }

    private void MovePlayer()
    {
        transform.Rotate(0f, horizontalInput * rotateSpeed, 0f);
        rb.AddForce(transform.forward * currentThrust, ForceMode.Force);
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        if(flatVel.magnitude > maxSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * maxSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }
}
