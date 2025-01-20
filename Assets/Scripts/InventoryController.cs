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
    [SerializeField] private GameObject itemPrefab;
    [SerializeField] private Transform itemsParent;
    [SerializeField] private TMPro.TextMeshProUGUI moneyText;
    [SerializeField] private Button sellButton;
    [SerializeField] private Button consumeButton;

    private Dictionary<IPickupable, int> items;
    private List<GameObject> itemObjects = new List<GameObject>();
    private IPickupable selectedItem;
    private GameObject selectedItemObject;

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
        moneyText.text = PlayerInventory.Money.ToString();
        gameObject.SetActive(false);
    }

    public void SellSelectedItem()
    {
        if (selectedItem == null || items[selectedItem] <= 0 || !IsOpenInShop) { return; }

        PlayerInventory.Money += (selectedItem as FishScriptableObject).price;
        items[selectedItem]--;
        selectedItemObject.transform.Find("Amount").GetComponent<TextMeshProUGUI>().text = items[selectedItem].ToString();
        moneyText.text = PlayerInventory.Money.ToString();
    }

    public void SelectItem(KeyValuePair<IPickupable, int> item, GameObject itemObject)
    {
        if(selectedItemObject != null)
            selectedItemObject.GetComponent<Image>().color = Color.gray;

        selectedItem = item.Key;
        selectedItemObject = itemObject;
        selectedItemObject.GetComponent<Image>().color = Color.green;
    }

    public void ConsumeSelectedItem()
    {
        if (selectedItem == null || items[selectedItem] <= 0 || selectedItem is not IConsumable) { return; }
        
        var b = (selectedItem as IConsumable).Consume();
        if (b)
        {
            items[selectedItem]--;
            selectedItemObject.transform.Find("Amount").GetComponent<TextMeshProUGUI>().text = items[selectedItem].ToString();
        }
        else
        {
            Debug.Log("Cannot consume item");
        }
    }
}
