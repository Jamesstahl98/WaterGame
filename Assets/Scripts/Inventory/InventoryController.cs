using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InventoryController : MonoBehaviour
{
    private ItemGrid _selectedItemGrid;

    [SerializeField] GameObject item0Size;

    public ItemGrid SelectedItemGrid
    {
        get => _selectedItemGrid;
        set
        {
            _selectedItemGrid = value;
            inventoryHighlight.SetParent(value);
        }
    }

    [HideInInspector] public InventoryItem selectedItem;
    InventoryItem overlapItem;
    RectTransform rectTransform;
    GameObject currentDescription;

    [SerializeField] GameObject itemPrefab;
    [SerializeField] Transform canvasTransform;
    private Canvas canvas;

    InventoryHighlight inventoryHighlight;

    private void Awake()
    {
        inventoryHighlight = GetComponent<InventoryHighlight>();
        canvas = canvasTransform.GetComponent<Canvas>();
        Vector2 pos;

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvasTransform as RectTransform, Input.mousePosition,
            canvas.worldCamera,
            out pos);
    }

    private void Update()
    {
        if (!canvasTransform.gameObject.active)
            return;

        ItemIconDrag();

        if (Input.GetKeyDown(KeyCode.R))
        {
            RotateItem();
        }

        if (_selectedItemGrid == null)
        {
            inventoryHighlight.Show(false);
            if (currentDescription != null && selectedItem == null)
            {
                currentDescription.SetActive(false);
            }
            return;
        }

        HandleHighlight();

        if (Input.GetMouseButtonDown(0))
        {
            InteractWithGrid();
        }
    }

    public void OnSceneRestart()
    {
        var tempPlacedItems = new List<InventoryItem>();
        for (int i = 0; i < StaticItemStats.PlacedItems.Count; i++)
        {
            tempPlacedItems.Add(StaticItemStats.PlacedItems[i]);
        }

        StaticItemStats.PlacedItems.Clear();

        for (int i = 0; i < StaticItemStats.NotPlacedItems.Count; i++)
        {

        }
    }

    private void RotateItem()
    {
        if (selectedItem == null) { return; }

        selectedItem.Rotate();
    }

    Vector2Int oldPosition;
    InventoryItem itemToHighlight;
    private void HandleHighlight()
    {
        Vector2Int positionOnGrid = GetTileGridPosition();

        if (oldPosition == positionOnGrid) { return; }

        oldPosition = positionOnGrid;
        if (selectedItem == null)
        {
            itemToHighlight = _selectedItemGrid.GetItem(positionOnGrid.x, positionOnGrid.y);

            if (itemToHighlight != null)
            {
                inventoryHighlight.Show(true);
                inventoryHighlight.SetSize(itemToHighlight);
                inventoryHighlight.SetPosition(_selectedItemGrid, itemToHighlight);
                if (currentDescription != null)
                    currentDescription.SetActive(false);

                currentDescription = itemToHighlight.ShowDescription(true);
            }
            else
            {
                inventoryHighlight.Show(false);
                if (currentDescription != null)
                    currentDescription.SetActive(false);
            }
        }
        else
        {
            inventoryHighlight.Show(_selectedItemGrid.BoundaryCheck(
                positionOnGrid.x,
                positionOnGrid.y,
                selectedItem.Width,
                selectedItem.Height)
                );

            inventoryHighlight.SetSize(selectedItem);
            inventoryHighlight.SetPosition(_selectedItemGrid, selectedItem, positionOnGrid.x, positionOnGrid.y);
        }
    }

    private void ItemIconDrag()
    {
        if (selectedItem != null)
        {
            Vector2 movePos;

            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                canvasTransform as RectTransform,
                Input.mousePosition, canvas.worldCamera,
                out movePos);

            rectTransform.position = canvasTransform.transform.TransformPoint(movePos);
        }
    }

    private void InteractWithGrid()
    {
        //Fix Controller support
        Vector2 position = Input.mousePosition;
        Vector2Int tileGridPosition = GetTileGridPosition();

        if (selectedItem == null)
        {
            PickUpItem(tileGridPosition);
        }
        else
        {
            PlaceItem(tileGridPosition);
        }
    }

    private Vector2Int GetTileGridPosition()
    {
        Vector2 position = Input.mousePosition;

        if (selectedItem != null)
        {
            position.x -= (selectedItem.Width - 1) * ItemGrid.tileSizeWidth / 2;
            position.y += (selectedItem.Height - 1) * ItemGrid.tileSizeHeight / 2;
        }

        return _selectedItemGrid.GetTileGridPosition(position);
    }

    private void PickUpItem(Vector2Int tileGridPosition)
    {
        selectedItem = _selectedItemGrid.PickUpItem(tileGridPosition.x, tileGridPosition.y);

        if (selectedItem != null)
        {
            rectTransform = selectedItem.GetComponent<RectTransform>();
            rectTransform.SetAsLastSibling();
            StaticItemStats.PlacedItems.Remove(selectedItem);
            StaticItemStats.NotPlacedItems.Add(selectedItem);
        }
    }

    private void PlaceItem(Vector2Int tileGridPosition)
    {
        bool complete = _selectedItemGrid.PlaceItem(selectedItem, tileGridPosition.x, tileGridPosition.y, ref overlapItem);
        if (complete)
        {
            StaticItemStats.PlacedItems.Add(selectedItem);
            StaticItemStats.NotPlacedItems.Remove(selectedItem);
            selectedItem = null;
            if (overlapItem != null)
            {
                selectedItem = overlapItem;
                overlapItem = null;
                StaticItemStats.PlacedItems.Remove(selectedItem);
                StaticItemStats.NotPlacedItems.Add(selectedItem);
                rectTransform = selectedItem.GetComponent<RectTransform>();
                rectTransform.SetAsLastSibling();
                currentDescription.SetActive(false);
                currentDescription = selectedItem.ShowDescription(true);
            }
        }
    }
}
