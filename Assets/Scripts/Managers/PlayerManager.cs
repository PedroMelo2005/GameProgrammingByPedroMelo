using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {
    [SerializeField] Player playerPrefab;

    // Create Instances
    public Player player;
    private GameManager gameManager;
    private MapManager mapManager;
    private ItemManager itemManager;
    private EnemyManager enemyManager;

    public void SetUpPlayerManager(GameManager gameManager, MapManager mapManager, ItemManager itemManager, EnemyManager enemyManager) {
        Debug.Log("SetUp the \"PlayerManager\"");
        this.gameManager = gameManager;
        this.mapManager = mapManager;
        this.itemManager = itemManager;
        this.enemyManager = enemyManager;
        player = Instantiate(playerPrefab, transform);
        player.SetUpPlayer(gameManager, mapManager, itemManager, enemyManager);
    }

    public void SpawnPlayer() {
        Debug.Log("Spawning the player"); // DEBUG
        player.transform.position = new Vector3(MapManager.StartingLocation, 2.1f, MapManager.StartingLocation);
    }

}