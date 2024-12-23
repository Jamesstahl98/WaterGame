using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Diagnostics;
using NUnit.Framework.Interfaces;

public class InventoryItem : MonoBehaviour
{
    private Transform gridTransform;
    private ItemGrid itemGrid;

    public ItemData itemData;

    public int Height
    {
        get
        {
            if (rotated == false)
            {
                return itemData.height;
            }
            return itemData.width;
        }
    }

    public int Width
    {
        get
        {
            if (rotated == false)
            {
                return itemData.width;
            }
            return itemData.height;
        }
    }

    public int onGridPositionX;
    public int onGridPositionY;

    public bool rotated = false;

    public GameObject descriptionObject;
    private ItemDescription descriptionScript;

    void Awake()
    {
        itemGrid = GameObject.Find("Grid").GetComponent<ItemGrid>();
    }

    internal void Set(ItemData itemData)
    {
        this.itemData = itemData;
        StaticItemStats.NotPlacedItems.Add(this);

        GetComponent<Image>().sprite = itemData.itemSprite;

        Vector2 size = new Vector2();
        size.x = itemData.width * ItemGrid.tileSizeWidth;
        size.y = itemData.height * ItemGrid.tileSizeHeight;
        GetComponent<RectTransform>().sizeDelta = size;

        //Create Description Object
        descriptionObject = Instantiate(itemData.descriptionObject, GameObject.Find("DescriptionPosition").transform);
        descriptionScript = descriptionObject.GetComponent<ItemDescription>();
        descriptionObject.transform.Find("DescriptionText").GetComponent<TextMeshProUGUI>().text = itemData.descriptionText;
        descriptionObject.transform.Find("FlavorText").GetComponent<TextMeshProUGUI>().text = itemData.flavorText;
        descriptionObject.transform.Find("Title").GetComponent<TextMeshProUGUI>().text = itemData.itemName;
    }

    internal void Rotate()
    {
        rotated = !rotated;

        RectTransform rectTransform = GetComponent<RectTransform>();
        rectTransform.rotation = Quaternion.Euler(0, 0, rotated == true ? 90f : 0f);
    }

    public GameObject ShowDescription(bool b)
    {
        descriptionScript.ShowDescription(b);
        return descriptionObject;
    }
}
