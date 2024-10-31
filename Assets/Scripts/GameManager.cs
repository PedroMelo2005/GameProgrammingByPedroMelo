using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    // Create Instances
    private Player _player; // Private "_player" instance
    private EnemyManager _enemyManager; // Private "_enemyManager" instance
    private ItemManager _itemManager; // Private "_itemManager" instance
    private Inventory _inventory; // Private "_inventory" instance
    private MapManager _gameMap; // Private "_gameMap" instance

    [SerializeField] Player PlayerPreFab;
    [SerializeField] EnemyManager EnemyManagerPreFab;
    [SerializeField] ItemManager ItemManagerPreFab;
    [SerializeField] Inventory InventoryManagerPrefab;
    [SerializeField] MapManager MapManagerPreFab;


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
        _player = Instantiate(PlayerPreFab, transform);
        _enemyManager = Instantiate(EnemyManagerPreFab, transform);
        _itemManager = Instantiate(ItemManagerPreFab, transform);
        _inventory = Instantiate(InventoryManagerPrefab, transform);
        _gameMap = Instantiate(MapManagerPreFab, transform);
    }
    
    void SetUpOnEverything() {
        // Call function SetUpMapManager
        _gameMap.SetUpMapManager();
        // Call function ResetPlayerStats
        _player.ResetPlayerStats();
    }

    void OnGameStart() {
        // Call function CreateMap
        _gameMap.CreateMap();
        // Call function VisualizeMap
        _gameMap.VisualizeMap();
    }

}