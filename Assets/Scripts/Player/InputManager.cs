using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private PlayerCollisionEvents playerCollisionEvents;
    [SerializeField] private GameObject InventoryObject;
    [SerializeField] private GameObject QuestLogObject;
    [SerializeField] private GameObject UpgradeMenuObject;
    [SerializeField] private CameraCustomScript cameraCustomScript;

    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        playerCollisionEvents = GetComponentInChildren<PlayerCollisionEvents>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        MovementInput();
        InteractInput();
        InventoryInput();
        QuestLogInput();
        UpgradeMenuInput();
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
            cameraCustomScript.ChangeAxisControl(!InventoryObject.activeSelf && !QuestLogObject.activeSelf);
            InventoryObject.GetComponent<InventoryController>().DeselectItem();
        }
    }

    private void QuestLogInput()
    {
        if(Input.GetKeyDown(KeyCode.L))
        {
            QuestLogObject.SetActive(!QuestLogObject.activeSelf);
            QuestLogObject.GetComponent<QuestLog>().RefreshQuestLog();
            cameraCustomScript.ChangeAxisControl(!QuestLogObject.activeSelf && !InventoryObject.activeSelf);
        }
    }

    private void UpgradeMenuInput()
    {
        if(Input.GetKeyDown(KeyCode.K))
        {
            UpgradeMenuObject.SetActive(!UpgradeMenuObject.activeSelf);
            cameraCustomScript.ChangeAxisControl(!UpgradeMenuObject.activeSelf);
        }
    }
}

