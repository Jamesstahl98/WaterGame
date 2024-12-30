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

    private Dictionary<IPickupable, int> items;
    private List<GameObject> itemObjects = new List<GameObject>();

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
            item.GetComponent<Button>().onClick.AddListener(() => { SellItem(entry, item); });
        }
        moneyText.text = PlayerInventory.Money.ToString();
        gameObject.SetActive(false);
    }

    public void SellItem(KeyValuePair<IPickupable, int> item, GameObject itemObject)
    {
        if (items[item.Key] <= 0 || !IsOpenInShop) { return; }

        PlayerInventory.Money += (item.Key as FishScriptableObject).price;
        items[item.Key]--;
        itemObject.transform.Find("Amount").GetComponent<TextMeshProUGUI>().text = items[item.Key].ToString();
        moneyText.text = PlayerInventory.Money.ToString();
    }
}
