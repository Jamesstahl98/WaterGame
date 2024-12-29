using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShopInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject userInterfaceGraphic;
    [SerializeField] private GameObject Inventory;

    public void PlayerEnteredTrigger()
    {
        userInterfaceGraphic.GetComponentInChildren<TextMeshProUGUI>().text = "Press E to open shop";
        userInterfaceGraphic.SetActive(true);
    }

    public void PlayerLeftTrigger()
    {
        userInterfaceGraphic.SetActive(false);
        Inventory.GetComponent<InventoryController>().IsOpenInShop = false;
    }

    public void Interact()
    {
        Inventory.SetActive(!Inventory.activeSelf);
        Inventory.GetComponent<InventoryController>().IsOpenInShop = true;
    }
}
