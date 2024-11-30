using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    [SerializeField] MapManager MapManagerPrefab;
    [SerializeField] PlayerManager PlayerManagerPrefab;
    [SerializeField] SpawnManager SpawnManagerPrefab;
    [SerializeField] ItemManager ItemManagerPrefab;
    [SerializeField] EnemyManager EnemyManagerPrefab;
    [SerializeField] UIManager UiManagerPrefab;

    // Create Instances
    private MapManager mapManager; // Private "mapManager" instance
    private PlayerManager playerManager; // Private "playerManager" instance
    private SpawnManager spawnManager; // Private "spawnManager" instance
    private ItemManager itemManager; // Private "itemManager" instance
    private EnemyManager enemyManager; // Private "enemyManager" instance
    private UIManager uiManager; // Private "uiManager" instance

    public static GameManager Instance; // Global static instance

    private bool _isGamePaused;
    public bool IsGamePaused => _isGamePaused;

    void Awake() {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start() {
        Debug.Log("Starting Game"); // DEBUG
        InstantiateManagers();
        SetUpEverything();
        OnGameStart();
    }

    // Update is called once per frame
    void Update() {

    }

    private void InstantiateManagers() {
        Debug.Log("Instantiating PreFabs of Player, EnemyManager, ItemManager and MapManager"); // DEBUG
        mapManager = Instantiate(MapManagerPrefab, transform);
        playerManager = Instantiate(PlayerManagerPrefab, transform);
        spawnManager = Instantiate(SpawnManagerPrefab, transform);
        itemManager = Instantiate(ItemManagerPrefab, transform);
        enemyManager = Instantiate(EnemyManagerPrefab, transform);
        uiManager = Instantiate(UiManagerPrefab);
    }

    private void SetUpEverything() {
        // Calling functions SetUp of Managers scripts
        mapManager.SetUpMapManager(this, playerManager, itemManager, enemyManager);
        playerManager.SetUpPlayerManager(this, mapManager, itemManager, enemyManager);
        spawnManager.SetUpSpawnManager(this, mapManager);
        itemManager.SetUpItemManager();
        enemyManager.SetUpEnemyManager();
        uiManager.SetUpUiManager(this, playerManager);
    }

    private void OnGameStart() {
        // Call the function "PauseGame"
        PauseGame(false);
        Debug.Log("starting create map"); // DEBUG
        // Call function CreateMap
        mapManager.CreateMap();
        Debug.Log("starting spawn player"); // DEBUG
        //Call function SpawnPlayer
        playerManager.SpawnPlayer();
        Debug.Log("Starting UIManager OnGameStart"); // DEBUG
        uiManager.OnStartGame();
        Debug.Log("Starting SpawnManager OnGameStart"); // DEBUG
        spawnManager.OnStartGame();
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