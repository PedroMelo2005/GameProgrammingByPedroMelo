using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomBase : MonoBehaviour {
    [SerializeField] private GameObject NorthDoorway, EastDoorway, SouthDoorway, WestDoorway;
    public PlayerManager playerManager;
    public InventoryManager inventoryManager;
    public ItemManager itemManager;
    public EnemyManager enemyManager;

    private RoomBase _north, _east, _south, _west;
    public RoomBase North => _north;
    public RoomBase East => _east;
    public RoomBase South => _south;
    public RoomBase West => _west;

    private Vector2 _roomPosition;
    public Vector2 RoomPosition => _roomPosition;

    public void SetUpRooms(PlayerManager playerManager, InventoryManager inventoryManager, ItemManager itemManager, EnemyManager enemyManager) {
        this.playerManager = playerManager;
        this.inventoryManager = inventoryManager;
        this.itemManager = itemManager;
        this.enemyManager = enemyManager;
    }

    public virtual void SetRoomLocation(Vector2 coordinates) {
        // X, Z plane
        transform.position = new Vector3(coordinates.x, 0, coordinates.y);
        _roomPosition = coordinates;
        Debug.Log("Room " + _roomPosition + " Created!"); // DEBUG
    }

    public void SetRooms(RoomBase roomNorth, RoomBase roomEast, RoomBase roomSouth, RoomBase roomWest) {
        _north = roomNorth;
        NorthDoorway.SetActive(_north == null);
        _east = roomEast;
        EastDoorway.SetActive(_east == null);
        _south = roomSouth;
        SouthDoorway.SetActive(_south == null);
        _west = roomWest;
        WestDoorway.SetActive(_west == null);
    }

    public static string itemFound = "";
    public virtual string roomName { get; }
    public virtual void OnRoomEntered() {
        // Display message of which room the player is in
        Debug.Log($"You entered in the {roomName}");
    }
    public virtual void OnRoomSearched() { }
    public virtual void OnRoomExit() {
        Debug.Log($"You left the {roomName}");
    }


}