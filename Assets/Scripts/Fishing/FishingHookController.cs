using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class FishingHookController : MonoBehaviour
{
    private bool hookDescending = true;
    private float horizontal;
    private float speed;
    private Vector2 fishingDepthTarget;
    private Rigidbody2D rb;

    [SerializeField] private float descendSpeed = 10f;
    [SerializeField] private float ascendSpeed = 10f;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float maxAscendSpeed;
    [SerializeField] private float acceleration;
    [SerializeField] private float deceleration;

    [Header("Torque")]
    [SerializeField] private float baseTorque = 3;
    [SerializeField] private float torqueModifier = 0.01f;
    [SerializeField] private float baseTorqueReduction = 1f;
    [SerializeField] private float torqueReductionModifier = 0.1f;
    [SerializeField] private float torqueReductionCap = 3f;
    [SerializeField] private float maxRotation = 60f;
    [SerializeField] private float torqueReductionFallOffModifier = 7f;
    [SerializeField] private float zRotationDifferenceThreshold = 0.35f;
    private float lastZRotation;

    [SerializeField] private GameObject AscendEarlyUI;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        fishingDepthTarget = new Vector2(0, -PlayerStats.FishingDepth);
    }

    void Update()
    {
        GetInput();
    }

    private void FixedUpdate()
    {
        if (hookDescending)
        {
            MoveTowardsYPosition(fishingDepthTarget);
            return;
        }
        Movement();
        AddTorque();
        CapZRotation();
        CapSpeed();
    }

    private void MoveTowardsYPosition(Vector2 targetPos)
    {
        float step = descendSpeed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, targetPos, step);

        if (transform.position.y <= targetPos.y)
        {
            StopDescent();
        }
    }

    private void GetInput()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space) && hookDescending)
        {
            StopDescent();
        }
    }

    private void StopDescent()
    {
        GetComponent<FishingHookColllision>().enabled = true;
        hookDescending = false;
        AscendEarlyUI.SetActive(false);
    }

    private void CapSpeed()
    {
        if (speed > maxSpeed)
        {
            speed = maxSpeed;
        }
        else if (speed < -maxSpeed)
        {
            speed = -maxSpeed;
        }

        if (rb.velocityY > maxAscendSpeed)
        {
            rb.velocityY = maxAscendSpeed;
        }
    }

    private void Movement()
    {
        if (horizontal == 1 || horizontal == -1)
        {
            speed += acceleration * horizontal;
        }

        else if (speed > 0)
        {
            speed -= deceleration;
        }

        else if (speed < 0)
        {
            speed += deceleration;
        }

        rb.velocity = new Vector2(speed, ascendSpeed);
    }

    private void AddTorque()
    {
        float zRotation = transform.eulerAngles.z;
        float torqueToAdd = 0;
        if (zRotation > 180) zRotation -= 360;

        if (horizontal == 1 && zRotation <= 0)
        {
            torqueToAdd = -baseTorque + (-zRotation * torqueModifier);
            if (torqueToAdd > 0)
            {
                torqueToAdd = 0;
            }
        }
        else if (horizontal == -1 && zRotation >= 0)
        {
            torqueToAdd = baseTorque - (zRotation * torqueModifier);
            if (torqueToAdd < 0)
            {
                torqueToAdd = 0;
            }
        }

        rb.AddTorque(torqueToAdd + GetTorqueReduction());

        if (horizontal == 0)
        {
            TryReduceTorqueAtThreshold(zRotation);
        }

        lastZRotation = zRotation;
    }

    private void TryReduceTorqueAtThreshold(float zRotation)
    {
        if (zRotation < 1 
            && zRotation > -1 
            && (Math.Abs(lastZRotation - zRotation) > zRotationDifferenceThreshold))
        {
            rb.AddTorque((lastZRotation + zRotation) * torqueReductionFallOffModifier);
        }
    }

    private float GetTorqueReduction()
    {
        float zRotation = transform.eulerAngles.z;
        float torqueReduction = 0;
        if (zRotation > 180) zRotation -= 360;

        if (zRotation > 0)
        {
            torqueReduction = torqueModifier - (zRotation * torqueReductionModifier);
            if (torqueReduction < -torqueReductionCap)
            {
                torqueReduction = -torqueReductionCap;
            }
        }
        else if (zRotation < 0)
        {
            torqueReduction = -torqueModifier + (-zRotation * torqueReductionModifier);
            if (torqueReduction > torqueReductionCap)
            {
                torqueReduction = torqueReductionCap;
            }
        }
        return torqueReduction;
    }

    private void CapZRotation()
    {
        float zRotation = transform.eulerAngles.z;
        if (zRotation > 180) zRotation -= 360;

        if (zRotation > maxRotation)
        {
            transform.rotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, maxRotation);
        }
        else if (zRotation < -maxRotation)
        {
            transform.rotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, -maxRotation);
        }
    }
}
