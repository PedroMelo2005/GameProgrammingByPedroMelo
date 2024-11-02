using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class InventoryManager : MonoBehaviour {
    [SerializeField] Inventory inventoryPrefab;

    // Create Instances
    private Inventory _inventory;
    ItemManager itemManager;

    public void SetUpInventoryManager(ItemManager itemManager) {
        Debug.Log("SetUp the \"InventoryManager\""); // DEBUG
        _inventory = Instantiate(inventoryPrefab, transform);
        this.itemManager = itemManager;
    }

}