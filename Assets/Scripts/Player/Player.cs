using System.Collections;
using System.Collections.Generic;
using Unity.Hierarchy;
using UnityEngine;

public class Player : MonoBehaviour {
    [SerializeField] private PlayerMovement _playerMovementScript;
    [SerializeField] private PlayerCamera _playerCameraScript;
    public Rigidbody PhysicsBody;

    // Create Instances
    public static Player Instance; // Global Static "Instance" instance of type "Player"
    MapManager mapManager;
    InventoryManager inventoryManager;
    ItemManager itemManager;
    EnemyManager enemyManager;

    public string PlayerName = "";
    public const int _maxPlayerLife = 50;
    public const int _minPlayerLife = 0;
    public int PlayerLife = _maxPlayerLife;
    public bool IsPlayerAlive = true;

    public void SetUpPlayer(MapManager mapManager, InventoryManager inventoryManager, ItemManager itemManager, EnemyManager enemyManager) {
        Debug.Log("SetUp the \"Player\"");
        this.mapManager = mapManager;
        this.inventoryManager = inventoryManager;
        this.itemManager = itemManager;
        this.enemyManager = enemyManager;
    }

    // Function GetPlayerLife that display the player's life
    public void GetPlayerLife() {
        Debug.Log($"{PlayerName} you have {PlayerLife} life points!");
    }

    // Function TakeDamage
    public void TakeDamage(int damageTaken) {
        PlayerLife -= damageTaken;

        // If PlayerLife is less or equal 0 the Player is dead
        if (PlayerLife <= _minPlayerLife) {
            IsPlayerAlive = false;
            PlayerLife = _minPlayerLife;
        }
    }

    // Function TakeHeal
    public void TakeHeal(int healTaken) {
        PlayerLife += healTaken;

        // If PlayerLife is greater than MaxPlayerLife the PlayerLife will be assigned to the MaxPlayerLife
        if (PlayerLife > _maxPlayerLife) {
            PlayerLife = _maxPlayerLife;
        }
    }

    // Function ResetPlayerStats that reset the player's life and player's inventory
    public void ResetPlayerStats() {
        Debug.Log("Resetting Player Stats"); // DEBUG
        PlayerLife = _maxPlayerLife;
        IsPlayerAlive = true;

        /*
        // If the "InventoryList" has 1 item or more will clear the "InventoryList"
        if (Inventory.Instance.InventoryList.Count > 0) {
        Inventory.Instance.InventoryList.Clear();
        }
        */
    }

}