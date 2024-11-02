using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {
    [SerializeField] Player playerPrefab;

    // Create Instances
    private Player _player;
    MapManager mapManager;
    InventoryManager inventoryManager;
    ItemManager itemManager;
    EnemyManager enemyManager;

    public void SetUpPlayerManager(MapManager mapManager, InventoryManager inventoryManager, ItemManager itemManager, EnemyManager enemyManager) {
        Debug.Log("SetUp the \"PlayerManager\"");
        _player = Instantiate(playerPrefab, transform);
        _player.SetUpPlayer(mapManager, inventoryManager, itemManager, enemyManager);
        this.mapManager = mapManager;
        this.inventoryManager = inventoryManager;
        this.enemyManager = enemyManager;
    }

}