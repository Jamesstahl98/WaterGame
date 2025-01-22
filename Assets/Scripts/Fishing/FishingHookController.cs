using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    [SerializeField] private GameObject AscendEarlyUI;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        fishingDepthTarget = new Vector2(0, -PlayerStats.FishingDepth);
    }

    void Update()
    {
        if(hookDescending)
        {
            MoveTowardsYPosition(fishingDepthTarget);
        }
        GetInput();
        CapSpeed();
    }

    private void FixedUpdate()
    {
        if (hookDescending) return;
        Movement();
    }

    private void MoveTowardsYPosition(Vector2 targetPos)
    {
        float step = descendSpeed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, targetPos, step);

        if(transform.position.y <= targetPos.y)
        {
            StopDescent();
        }
    }

    private void GetInput()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        if(Input.GetKeyDown(KeyCode.Space) && hookDescending)
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
        if(speed > maxSpeed)
        {
            speed = maxSpeed;
        }
        else if(speed < -maxSpeed)
        {
            speed = -maxSpeed;
        }

        if(rb.velocityY > maxAscendSpeed)
        {
            rb.velocityY = maxAscendSpeed;
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

        rb.velocity = new Vector2(speed, ascendSpeed);
    }
}
