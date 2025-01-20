using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiInventoryBar : MonoBehaviour
{
    [SerializeField] private Sprite blankSprite;
    [SerializeField] private UiInventorySlot[] inventorySlots;
    public GameObject inventoryBarDraggedItem;
    [HideInInspector] public GameObject inventoryTextBoxGameobject;

    private RectTransform rectTransform;

    private bool _isInventoryBarPositionBottom = true;

    public bool isInventoryBarPositionBottom { get => _isInventoryBarPositionBottom; set => _isInventoryBarPositionBottom = value; }


    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    private void Update()
    {
        SwitchInventoryBarPosition();
    }

    private void OnEnable()
    {
        EventHandler.InventoryUpdatedEvent += InventoryUpdated;
    }

    private void OnDisable()
    {
        EventHandler.InventoryUpdatedEvent -= InventoryUpdated;
    }

    private void InventoryUpdated(InventoryLocation inventoryLocation, List<InventoryItem> inventoryList)
    {
        if(inventoryLocation == InventoryLocation.player)
        {
            ClearInventorySlots();

            if(inventoryList.Count > 0 && inventorySlots.Length > 0)
            {
                for(int i = 0; i  < inventorySlots.Length; i++)
                {
                    if(i < inventoryList.Count)
                    {
                        int itemCode = inventoryList[i].itemCode;

                        ItemDetails itemDetails = InventoryManager.Instance.GetItemDetails(itemCode);


                        if(itemDetails != null)
                        {
                            inventorySlots[i].itemDetails = itemDetails;
                            inventorySlots[i].textMeshProUGUI.text = inventoryList[i].itemQuantity.ToString();
                            inventorySlots[i].inventorySlotImage.sprite = itemDetails.itemSprite;
                            inventorySlots[i].itemQuantity = inventoryList[i].itemQuantity;
                            SetHighlightedInventorySlots(i);
                        }
                    }
                    else
                    {
                        break;
                    }
                }
            }

        }
    }

    private void ClearInventorySlots()
    {
        if (inventorySlots.Length > 0)
        {
            for (int i = 0; i < inventorySlots.Length; i++)
            {
                inventorySlots[i].itemQuantity = 0;
                inventorySlots[i].itemDetails = null;
                inventorySlots[i].inventorySlotImage.sprite = blankSprite;
                inventorySlots[i].textMeshProUGUI.text = "";

                SetHighlightedInventorySlots(i);
            }
        }
    }

    private void SwitchInventoryBarPosition()
    {
        Vector3 playerViewPortPosition = Player.Instance.GetPlayerViewPortPosition();

        Debug.Log(playerViewPortPosition);
        if(playerViewPortPosition.y > 0.3f && isInventoryBarPositionBottom == false)
        {
            rectTransform.pivot = new Vector2(0.5f, 0);
            rectTransform.anchorMax = new Vector2(0.5f, 0);
            rectTransform.anchorMin = new Vector2(0.5f, 0);

            rectTransform.anchoredPosition = new Vector2(0f, 2.5f);

            isInventoryBarPositionBottom = true;
        }
        else if(playerViewPortPosition.y <= 0.3f && isInventoryBarPositionBottom == true)
        {
            rectTransform.pivot = new Vector2(0.5f, 1f);
            rectTransform.anchorMax = new Vector2(0.5f, 1f);
            rectTransform.anchorMin = new Vector2(0.5f, 1f);

            rectTransform.anchoredPosition = new Vector2(0f, -2.5f);

            isInventoryBarPositionBottom = false;
        }
    }

    public void ClearHighlightOnInventorySlots()
    {
        if(inventorySlots.Length > 0)
        {
            for(int i = 0; i< inventorySlots.Length; i++)
            {
                inventorySlots[i].isSelected = false;
                inventorySlots[i].inventorySlotHighlight.color = new Color(0f, 0f, 0f, 0f);

                InventoryManager.Instance.ClearSelectedInventoryItem(InventoryLocation.player);
            }
        }
    }

    public void SetHighlightedInventorySlots()
    {
        if (inventorySlots.Length > 0)
        {
            for(int i = 0; i< inventorySlots.Length;i++)
            {
                SetHighlightedInventorySlots(i);
            }
        }
    }

    private void SetHighlightedInventorySlots(int itemPosition)
    {
        if (inventorySlots.Length > 0 && inventorySlots[itemPosition].itemDetails != null)
        {
            if (inventorySlots[itemPosition].isSelected)
            {
                inventorySlots[itemPosition].inventorySlotHighlight.color = new Color(1f, 1f, 1f, 1f);


                InventoryManager.Instance.SetSelectedInventoryItem(InventoryLocation.player, inventorySlots[itemPosition].itemDetails.itemCode);
            }
        }
    }
}
