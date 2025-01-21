using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Xml;
using TMPro;
using UnityEngine;
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

        PlayerInventory.Money -= selectedShopItem.GetBuyPrice();
        
        //PlayerInventory.ItemsInInventory.Add(selectedShopItem, 1);

        moneyText.text = PlayerInventory.Money.ToString();
        
        if(items.ContainsKey(selectedShopItem))
        {
            items[selectedShopItem]++;
            selectedShopItemObject.transform.Find("Amount").GetComponent<TextMeshProUGUI>().text = items[selectedShopItem].ToString();
        }
        else
        {
            items.Add(selectedShopItem, 1);
            var item = Instantiate(itemPrefab, itemsParent);
            item.transform.Find("Amount").GetComponent<TextMeshProUGUI>().text = items[selectedShopItem].ToString();
            item.transform.Find("Icon").GetComponent<Image>().sprite = selectedShopItem.GetSprite();
            item.transform.Find("ItemName").GetComponent<TextMeshProUGUI>().text = selectedShopItem.GetName();
            itemObjects.Add(item);
            item.GetComponent<Button>().onClick.AddListener(() => { SelectItem(new KeyValuePair<IPickupable, int>(selectedShopItem, items[selectedShopItem]), item); });
        }
    }

    public void SellSelectedItem()
    {
        if (selectedItem == null || items[selectedItem] <= 0 || !IsOpenInShop) { return; }

        PlayerInventory.Money += selectedItem.GetSellPrice();
        items[selectedItem]--;

        if (items[selectedItem] <= 0)
        {
            items.Remove(selectedItem);
            Destroy(selectedItemObject);
        }
        else
        {
            selectedItemObject.transform.Find("Amount").GetComponent<TextMeshProUGUI>().text = items[selectedItem].ToString();
        }
        moneyText.text = PlayerInventory.Money.ToString();
        selectedItem = null;
    }

    public void SelectItem(KeyValuePair<IPickupable, int> item, GameObject itemObject)
    {
        if(selectedItemObject != null)
            selectedItemObject.GetComponent<Image>().color = Color.gray;

        selectedItem = item.Key;
        selectedItemObject = itemObject;
        selectedItemObject.GetComponent<Image>().color = Color.green;
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
}
