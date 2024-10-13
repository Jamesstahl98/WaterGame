using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingHookController : MonoBehaviour
{
    private float horizontal;
    private float speed;
    private Rigidbody2D rb;

    [SerializeField] private float maxSpeed;
    [SerializeField] private float acceleration;
    [SerializeField] private float deceleration;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        GetInput();
        CapSpeed();
    }

    private void FixedUpdate()
    {
        Movement();
    }

    private void GetInput()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
    }

    private void CapSpeed()
    {
        if(speed > maxSpeed)
        {
            speed = maxSpeed;
        }
        else if(speed < -maxSpeed)
        {
            speed = -maxSpeed;
        }
    }

    private void Movement()
    {
        if(horizontal == 1 || horizontal == -1)
        {
            speed += acceleration * horizontal;
        }

        else if(speed > 0)
        {
            speed -= deceleration;
        }

        else if(speed < 0)
        {
            speed += deceleration;
        }

        rb.velocity = new Vector2(speed, rb.velocityY);
    }
}
