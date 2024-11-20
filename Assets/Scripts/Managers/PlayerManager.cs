using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {
    [SerializeField] Player playerPrefab;

    // Create Instances
    public Player player;
    public MapManager mapManager;
    public ItemManager itemManager;
    public EnemyManager enemyManager;

    public void SetUpPlayerManager(MapManager mapManager, ItemManager itemManager, EnemyManager enemyManager) {
        Debug.Log("SetUp the \"PlayerManager\"");
        player = Instantiate(playerPrefab, transform);
        player.SetUpPlayer(mapManager, itemManager, enemyManager);
        this.mapManager = mapManager;
        this.enemyManager = enemyManager;
    }

    public void SpawnPlayer() {
        Debug.Log("Spawning the player"); // DEBUG
        player.transform.position = new Vector3(MapManager.StartingLocation, 2.1f, MapManager.StartingLocation);
    }

}