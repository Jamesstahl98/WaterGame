using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private PlayerCollisionEvents playerCollisionEvents;
    [SerializeField] private GameObject InventoryObject;
    [SerializeField] private GameObject QuestLogObject;

    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        playerCollisionEvents = GetComponentInChildren<PlayerCollisionEvents>();
    }

    void Update()
    {
        MovementInput();
        InteractInput();
        InventoryInput();
        QuestLogInput();
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
            InventoryObject.SetActive(!InventoryObject.activeSelf);
        }
    }

    private void QuestLogInput()
    {
        if(Input.GetKeyDown(KeyCode.L))
        {
            QuestLogObject.SetActive(!QuestLogObject.activeSelf);
            QuestLogObject.GetComponent<QuestLog>().RefreshQuestLog();
        }
    }
}

