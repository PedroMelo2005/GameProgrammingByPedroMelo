using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class ItemData {
    public Item item;
    public bool isRotated = false;
    public Vector2Int matrixPosition;
    public Vector3 slotPosition = Vector3.zero;
    public InventoryPanel.Type slotPanelType;
    public GameObject myLootContainer;
    public int myLootContainerId;
}