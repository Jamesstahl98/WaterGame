using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private PlayerCollisionEvents playerCollisionEvents;
    private GameObject InventoryCanvas;

    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        playerCollisionEvents = GetComponentInChildren<PlayerCollisionEvents>();
        InventoryCanvas = GameObject.Find("InventoryCanvas");
    }

    void Update()
    {
        MovementInput();
        InteractInput();
        InventoryInput();
    }

    private void MovementInput()
    {
        playerMovement.VerticalInput = Input.GetAxisRaw("Vertical");
        playerMovement.HorizontalInput = Input.GetAxisRaw("Horizontal");
    }
    private void InteractInput()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            playerCollisionEvents.Interact();
        }
    }
    private void InventoryInput()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            InventoryCanvas.SetActive(!InventoryCanvas.activeSelf);
        }
    }
}
