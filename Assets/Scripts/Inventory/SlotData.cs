using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SlotData : MonoBehaviour, IDropHandler, IPointerEnterHandler {
    public Item.SlotType slotType;
    public Vector2Int matrixPosition;
    public InventoryPanel.Type inventoryPanelType;
    public GameObject myLootContainer;
    public bool isFull = false; // For the character panel

    public void OnDrop(PointerEventData eventData) {
        //
    }

    public void OnPointerEnter(PointerEventData eventData) {
        //
    }

}