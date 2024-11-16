using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour {
    [SerializeField] Inventory inventoryPrefab;
    [SerializeField] Sprite[] spritesItem;

    // Create Instances
    private Inventory _inventory;
    ItemManager itemManager;

    public void SetUpInventoryManager(ItemManager itemManager) {
        Debug.Log("SetUp the \"InventoryManager\""); // DEBUG
        _inventory = Instantiate(inventoryPrefab, transform);
        this.itemManager = itemManager;
    }

}