using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    [SerializeField] MapManager MapManagerPrefab;
    [SerializeField] PlayerManager PlayerManagerPrefab;
    [SerializeField] InventoryManager InventoryManagerPrefab;
    [SerializeField] ItemManager ItemManagerPrefab;
    [SerializeField] EnemyManager EnemyManagerPrefab;

    // Create Instances
    private MapManager _mapManager; // Private "_gameMap" instance
    private PlayerManager _playerManager; // Private "_player" instance
    private InventoryManager _inventoryManager; // Private "_inventory" instance
    private ItemManager _itemManager; // Private "_itemManager" instance
    private EnemyManager _enemyManager; // Private "_enemyManager" instance

    // Start is called before the first frame update
    public void Start() {
        Debug.Log("Starting Game"); // DEBUG
        InstantiateInstances();
        SetUpOnEverything();
        OnGameStart();
    }

    // Update is called once per frame
    void Update() {

    }

    void InstantiateInstances() {
        Debug.Log("Instantiating PreFabs of Player, EnemyManager, ItemManager and MapManager"); // DEBUG
        _mapManager = Instantiate(MapManagerPrefab, transform);
        _playerManager = Instantiate(PlayerManagerPrefab, transform);
        _inventoryManager = Instantiate(InventoryManagerPrefab, transform);
        _itemManager = Instantiate(ItemManagerPrefab, transform);
        _enemyManager = Instantiate(EnemyManagerPrefab, transform);
    }
    
    void SetUpOnEverything() {
        // Calling functions SetUp of Managers scripts
        _mapManager.SetUpMapManager(_playerManager, _inventoryManager, _itemManager, _enemyManager);
        _playerManager.SetUpPlayerManager(_mapManager, _inventoryManager, _itemManager, _enemyManager);
        _inventoryManager.SetUpInventoryManager(_itemManager);
        _itemManager.SetUpItemManager();
        _enemyManager.SetUpEnemyManager();
    }

    void OnGameStart() {
        // Call function CreateMap
        _mapManager.CreateMap();
        //Call function SpawnPlayer
        _playerManager.SpawnPlayer();
    }

}