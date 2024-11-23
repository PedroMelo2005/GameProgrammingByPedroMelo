using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    [SerializeField] MapManager MapManagerPrefab;
    [SerializeField] PlayerManager PlayerManagerPrefab;
    [SerializeField] SpawnManager SpawnManagerPrefab;
    [SerializeField] ItemManager ItemManagerPrefab;
    [SerializeField] EnemyManager EnemyManagerPrefab;

    // Create Instances
    private MapManager _mapManager; // Private "_gameMap" instance
    private PlayerManager _playerManager; // Private "_player" instance
    private SpawnManager _spawnManager; // Private "_spawnManager" instance
    private ItemManager _itemManager; // Private "_itemManager" instance
    private EnemyManager _enemyManager; // Private "_enemyManager" instance

    public static GameManager Instance; // Global static instance

    private bool _isGamePaused;
    public bool IsGamePaused { get { return _isGamePaused; } }

    void Awake() {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start() {
        Debug.Log("Starting Game"); // DEBUG
        InstantiateInstances();
        SetUpOnEverything();
        OnGameStart();
    }

    // Update is called once per frame
    void Update() {

    }

    private void InstantiateInstances() {
        Debug.Log("Instantiating PreFabs of Player, EnemyManager, ItemManager and MapManager"); // DEBUG
        _mapManager = Instantiate(MapManagerPrefab, transform);
        _playerManager = Instantiate(PlayerManagerPrefab, transform);
        _spawnManager = Instantiate(SpawnManagerPrefab, transform);
        _itemManager = Instantiate(ItemManagerPrefab, transform);
        _enemyManager = Instantiate(EnemyManagerPrefab, transform);
    }

    private void SetUpOnEverything() {
        // Calling functions SetUp of Managers scripts
        _mapManager.SetUpMapManager(_playerManager, _itemManager, _enemyManager);
        _playerManager.SetUpPlayerManager(_mapManager, _itemManager, _enemyManager);
        _itemManager.SetUpItemManager();
        _enemyManager.SetUpEnemyManager();
        _spawnManager.SetUp(_mapManager);
    }

    private void OnGameStart() {
        // Call the function "PauseGame"
        PauseGame(false);
        // Call function CreateMap
        _mapManager.CreateMap();
        //Call function SpawnPlayer
        _playerManager.SpawnPlayer();
    }

    public void PauseGame(bool pauseState) {
        // Assign the variable "IsGamePaused" to "pauseState"
        _isGamePaused = pauseState;

        if (pauseState) {
            Time.timeScale = 0f;
        }
        else {
            Time.timeScale = 1f;
        }
    }

}