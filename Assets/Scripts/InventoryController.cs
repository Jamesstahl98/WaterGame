using System.Collections;
using System.Collections.Generic;
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

        sellButton.onClick.AddListener(SellItem);
        moneyText.text = PlayerInventory.Money.ToString();
        gameObject.SetActive(false);
    }

    public void SellItem()
    {
        if (items[selectedItem] <= 0 || !IsOpenInShop) { return; }

        PlayerInventory.Money += (selectedItem as FishScriptableObject).price;
        items[selectedItem]--;
        selectedItemObject.transform.Find("Amount").GetComponent<TextMeshProUGUI>().text = items[selectedItem].ToString();
        moneyText.text = PlayerInventory.Money.ToString();
    }

    public void SelectItem(KeyValuePair<IPickupable, int> item, GameObject itemObject)
    {
        if(selectedItemObject != null)
            selectedItemObject.GetComponent<Image>().color = Color.white;

        selectedItem = item.Key;
        selectedItemObject = itemObject;
        selectedItemObject.GetComponent<Image>().color = Color.green;
    }
}
