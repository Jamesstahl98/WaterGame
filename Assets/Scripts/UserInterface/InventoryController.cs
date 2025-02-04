using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Xml;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
using static UnityEngine.EventSystems.EventTrigger;

public class InventoryController : MonoBehaviour
{
    private List<GameObject> itemObjects = new List<GameObject>();
    [Header("PlayerInventory")]
    [SerializeField] private GameObject itemPrefab;
    [SerializeField] private Transform itemsParent;
    [SerializeField] private TMPro.TextMeshProUGUI moneyText;
    [SerializeField] private Button sellButton;
    [SerializeField] private Button consumeButton;
    [SerializeField] private GameObject itemDescriptionObject;
    [SerializeField] private Color itemBackgroundColor;
    private Dictionary<IPickupable, int> items;
    private IPickupable selectedItem;
    private GameObject selectedItemObject;

    [Header("ShopInventory")]
    [SerializeField] private Button buyButton;
    [SerializeField] private Transform shopItemsParent;
    [SerializeField] private GameObject buyItemContainer;
    private IPickupable selectedShopItem;
    private GameObject selectedShopItemObject;

    public bool IsOpenInShop = false;

    private void Awake()
    {
        items = PlayerInventory.ItemsInInventory;

        foreach (KeyValuePair<IPickupable, int> entry in items)
        {
            var item = Instantiate(itemPrefab, itemsParent);
            item.transform.Find("Amount").GetComponent<TextMeshProUGUI>().text = entry.Value.ToString();
            item.transform.Find("Icon").GetComponent<Image>().sprite = entry.Key.GetSprite();
            item.transform.Find("ItemName").GetComponent<TextMeshProUGUI>().text = entry.Key.GetName();
            itemObjects.Add(item);
            item.GetComponent<Button>().onClick.AddListener(() => { SelectItem(entry, item); });
        }

        sellButton.onClick.AddListener(SellSelectedItem);
        consumeButton.onClick.AddListener(ConsumeSelectedItem);
        buyButton.onClick.AddListener(BuySelectedItem);
        moneyText.text = PlayerInventory.Money.ToString();
        gameObject.SetActive(false);
    }

    private void BuySelectedItem()
    {
        if (selectedShopItem == null || PlayerInventory.Money < selectedShopItem.GetBuyPrice()) { return; }

        UpdateMoney(-selectedShopItem.GetBuyPrice());

        AddItemToInventory(selectedShopItem);
    }

    public void SellSelectedItem()
    {
        if (selectedItem == null || items[selectedItem] <= 0 || !IsOpenInShop) { return; }

        UpdateMoney(selectedItem.GetSellPrice());
        items[selectedItem]--;

        if (items[selectedItem] <= 0)
        {
            items.Remove(selectedItem);
            Destroy(selectedItemObject);
            DeselectItem();
        }
        else
        {
            selectedItemObject.transform.Find("Amount").GetComponent<TextMeshProUGUI>().text = items[selectedItem].ToString();
        }

        selectedItem = null;
    }

    public void AddItemToInventory(IPickupable item)
    {
        if (items.ContainsKey(item))
        {
            items[item]++;
            itemObjects.Find(x => x.transform.Find("ItemName").GetComponent<TextMeshProUGUI>().text == item.GetName()).transform.Find("Amount").GetComponent<TextMeshProUGUI>().text = items[item].ToString();
        }
        else
        {
            items.Add(item, 1);
            var newItemObject = Instantiate(itemPrefab, itemsParent);
            newItemObject.transform.Find("Amount").GetComponent<TextMeshProUGUI>().text = items[item].ToString();
            newItemObject.transform.Find("Icon").GetComponent<Image>().sprite = item.GetSprite();
            newItemObject.transform.Find("ItemName").GetComponent<TextMeshProUGUI>().text = item.GetName();
            itemObjects.Add(newItemObject);
            newItemObject.GetComponent<Button>().onClick.AddListener(() => { SelectItem(new KeyValuePair<IPickupable, int>(item, items[item]), newItemObject); });
        }
    }

    public void CheckForEmptyItemSlots()
    {
        List<IPickupable> itemsToRemove = new List<IPickupable>();

        foreach (var item in items)
        {
            if (item.Value <= 0)
            {
                itemsToRemove.Add(item.Key);
            }
        }
        
        foreach (var item in itemsToRemove)
        {
            var itemObject = itemObjects.Find(x => x.transform.Find("ItemName").GetComponent<TextMeshProUGUI>().text == item.GetName());
            if(itemObject != null)
            {
                Destroy(itemObject);
            }

            items.Remove(item);
        }
    }
    public void UpdateMoney(int moneyChange)
    {
        PlayerInventory.Money += moneyChange;
        moneyText.text = PlayerInventory.Money.ToString();
    }

    public void SelectItem(KeyValuePair<IPickupable, int> item, GameObject itemObject)
    {
        if(selectedItemObject != null)
            selectedItemObject.GetComponent<Image>().color = itemBackgroundColor;

        selectedItem = item.Key;
        selectedItemObject = itemObject;
        selectedItemObject.GetComponent<Image>().color = Color.green;
        itemDescriptionObject.SetActive(true);
        itemDescriptionObject.transform.Find("ItemName").GetComponent<TextMeshProUGUI>().text = item.Key.GetName();
        itemDescriptionObject.transform.Find("ItemDescription").GetComponent<TextMeshProUGUI>().text = item.Key.GetDescription();
    }

    public void DeselectItem()
    {
        if(selectedItemObject != null)
        {
            selectedItemObject.GetComponent<Image>().color = itemBackgroundColor;
        }
        selectedItem = null;
        selectedItemObject = null;
        itemDescriptionObject.SetActive(false);
    }

    public void SelectItemInShop(IPickupable item, GameObject itemObject)
    {
        if (selectedShopItemObject != null)
            selectedShopItemObject.GetComponent<Image>().color = Color.gray;

        selectedShopItem = item;
        selectedShopItemObject = itemObject;
        selectedShopItemObject.GetComponent<Image>().color = Color.green;
    }

    public void ConsumeSelectedItem()
    {
        if (selectedItem == null || items[selectedItem] <= 0 || selectedItem is not IConsumable) { return; }
        var b = (selectedItem as IConsumable).Consume();
        if (b)
        {
            items[selectedItem]--;
            if (items[selectedItem] <= 0)
            {
                items.Remove(selectedItem);
                Destroy(selectedItemObject);
                DeselectItem();
            }
            else
            {
                selectedItemObject.transform.Find("Amount").GetComponent<TextMeshProUGUI>().text = items[selectedItem].ToString();
            }
        }
        else
        {
            Debug.Log("Cannot consume item");
        }
    }
    public void UpdateShopInventory(List<ScriptableObject> shopItems = null)
    {
        for (var i = shopItemsParent.transform.childCount - 1; i >= 0; i--)
        {
            Destroy(shopItemsParent.transform.GetChild(i).gameObject);
        }
        if (shopItems == null)
        {
            buyItemContainer.SetActive(false);
            buyButton.gameObject.SetActive(false);
            return;
        }
        else
        {
            buyItemContainer.SetActive(true);
            buyButton.gameObject.SetActive(true);
        }
        foreach (IPickupable entry in shopItems)
        {
            var item = Instantiate(itemPrefab, shopItemsParent);
            item.transform.Find("Icon").GetComponent<Image>().sprite = entry.GetSprite();
            item.transform.Find("ItemName").GetComponent<TextMeshProUGUI>().text = entry.GetName();
            item.GetComponent<Button>().onClick.AddListener(() => { SelectItemInShop(entry, item); });
        }
    }

    internal void RemoveItem(IPickupable item, int amount)
    {
        items[item] -= amount;
    }
}
