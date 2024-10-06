using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    private Transform playerOrientation;
    private Transform playerObj;
    private Rigidbody rb;

    [SerializeField] private float rotationSpeed;

    void Awake()
    {
        playerOrientation = playerTransform.Find("Orientation");
        playerObj = playerTransform.Find("PlayerObj");
        rb = playerTransform.GetComponent<Rigidbody>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        Vector3 viewDir = playerTransform.position - new Vector3(transform.position.x, playerTransform.position.x, transform.position.z);
        playerOrientation.forward = viewDir.normalized;

        //float horizontalInput = Input.GetAxis("Horizontal");
        //float verticalInput = Input.GetAxis("Vertical");
        //Vector3 inputDir = playerOrientation.forward * verticalInput + playerOrientation.right * horizontalInput;

        //if(inputDir != Vector3.zero)
        //{
        //    playerObj.forward = Vector3.Slerp(playerObj.forward, inputDir.normalized, Time.deltaTime * rotationSpeed);
        //}
    }
}
