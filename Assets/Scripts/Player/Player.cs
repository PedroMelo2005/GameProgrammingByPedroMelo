using System.Collections;
using System.Collections.Generic;
using Unity.Hierarchy;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour {
    [SerializeField] private PlayerInputs _playerInputs;
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private PlayerCamera _playerCamera;
    [SerializeField] private PlayerInteraction _playerInteraction;
    public Rigidbody PhysicsBody;

    // Create Instances
    public static Player Instance; // Global Static "instance" of type "Player"

    private GameManager gameManager;
    private MapManager mapManager;
    private ItemManager itemManager;
    private EnemyManager enemyManager;

    public string PlayerName = "";
    public const float _maxPlayerLife = 50f;
    public const float _minPlayerLife = 0f;
    public float PlayerLife = _maxPlayerLife;
    public bool IsPlayerAlive;
    public bool PlayerCanMove;

    private void Awake() {
        Instance = this;
    }

    public void SetUpPlayer(GameManager gameManager, MapManager mapManager, ItemManager itemManager, EnemyManager enemyManager) {
        Debug.Log("SetUp the \"Player\"");
        // Call function ResetPlayerStats
        ResetPlayerStats();
        this.gameManager = gameManager;
        this.mapManager = mapManager;
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

    public void ActivateCursor() {
        // Unlock the Cursor and set "Cursor.visible" to true
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        PlayerCanMove = false;
    }

    public void DeactivateCursor() {
        // Lock the Cursor to the center of the game window and set "Cursor.visible" to false
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        PlayerCanMove = true;
    }

}