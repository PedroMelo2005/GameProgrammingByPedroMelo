using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder.MeshOperations;

public class MapManager : MonoBehaviour {
    [SerializeField] RoomBase welcomeRoomPrefab;
    [SerializeField] RoomBase combatRoomPrefab;
    [SerializeField] RoomBase treasureRoomPrefab;
    [SerializeField] RoomBase puzzleRoomPrefab;

    public const int MapSize = 5; // size of the map
    public const int RoomSize = 10; // size of the rooms prefab
    public const float StartingLocation = (((MapSize - 1) * RoomSize) / 2); // Location at the center of the map

    readonly Dictionary<Vector2, RoomBase> _rooms = new();
    public Dictionary<Vector2, RoomBase> Rooms => _rooms;

    // Create a List "roomList" of type GameObject
    List<RoomBase> roomList;

    public void SetUpMapManager(PlayerManager playerManager, InventoryManager inventoryManager, ItemManager itemManager, EnemyManager enemyManager) {
        Debug.Log("SetUp the \"MapManager\""); // DEBUG
        // Initialize the "roomList"
        Debug.Log("Initialize the \"roomList\""); // DEBUG
        roomList = new List<RoomBase> { combatRoomPrefab, treasureRoomPrefab, puzzleRoomPrefab };
    }

    public void CreateMap() {
        int midPoint = ((MapSize - 1) / 2); // "midPoint" represents the center of the grid

        for (int x = 0; x < MapSize; x++) {
            for (int z = 0; z < MapSize; z++) {
                Vector2 coords = new Vector2(x * RoomSize, z * RoomSize);
                RoomBase roomInstance = null;

                // If is in on the center of the grid will run this code
                if (x == midPoint && z == midPoint) {
                    roomInstance = Instantiate(welcomeRoomPrefab, transform);
                }
                // Others rooms that are not on the center of the grid
                else {
                    roomInstance = Instantiate(selectedRoomPrefab(), transform);
                }
                roomInstance.SetRoomLocation(coords);
                _rooms.Add(coords, roomInstance);
            }
        }

        foreach (var roomByCoordinate in _rooms) {
            RoomBase northRoom = FindRoom(roomByCoordinate.Key, Direction.North);
            RoomBase eastRoom = FindRoom(roomByCoordinate.Key, Direction.East);
            RoomBase southRoom = FindRoom(roomByCoordinate.Key, Direction.South);
            RoomBase westRoom = FindRoom(roomByCoordinate.Key, Direction.West);

            roomByCoordinate.Value.SetRooms(northRoom, eastRoom, southRoom, westRoom);
        }
    }

    private RoomBase FindRoom(Vector2 currentRoom, Direction direction) {
        RoomBase room = null;
        Vector2 nextRoomCoordinates = new Vector2(-1, -1);

        switch (direction) {
            case Direction.North:
                // Determine North Room
                nextRoomCoordinates = currentRoom + (Vector2.up * RoomSize);
                break;
            case Direction.East:
                // east
                nextRoomCoordinates = currentRoom + (Vector2.right * RoomSize);
                break;
            case Direction.South:
                // south
                nextRoomCoordinates = currentRoom + (Vector2.down * RoomSize);
                break;
            case Direction.West:
                // west
                nextRoomCoordinates = currentRoom + (Vector2.left * RoomSize);
                break;
        }

        if (_rooms.TryGetValue(nextRoomCoordinates, out var nextRoom)) {
            room = nextRoom;
        }

        return room;
    }

    public void OldCreateMap() {
        // midPoint represents the center of the grid
        int midPoint = (MapSize - 1) / 2;

        Debug.Log("Starting loop to CreateMap"); // DEBUG
        // Run the code while the variable "x" is less than the "MapSize"
        for (int x = 0; x < MapSize; x++) {
            // Run the code while the variable "z" is less than the "MapSize"
            for (int z = 0; z < MapSize; z++) {

                // Create a variable "coords" that will calculate the coordinates X and Y of the room
                Vector2 coords = new Vector2(x * RoomSize, z * RoomSize);

                // If is in on the center of the grid will run this code
                if (x == midPoint && z == midPoint) {
                    var startRoomInstance = Instantiate(welcomeRoomPrefab, transform);
                    startRoomInstance.transform.position = new Vector3(coords.x, 0, coords.y);
                }
                // Others rooms that are not on the center of the grid
                else {
                    var randomRoomInstance = Instantiate(selectedRoomPrefab(), transform);
                    randomRoomInstance.transform.position = new Vector3(coords.x, 0, coords.y);
                }
            }
        }
        Debug.Log("Ending loop to CreateMap"); // DEBUG
    }

    public void VisualizeMap() {
        Debug.Log("Starting loop to VisualizeMap"); // DEBUG
        for (int x = 0; x < MapSize; x++) {
            for (int z = 0; z < MapSize; z++) {
                GameObject mapRoomRepresentation = GameObject.CreatePrimitive(PrimitiveType.Cube);
                mapRoomRepresentation.transform.position = new Vector3(x, 0, z);
            }
        }
        Debug.Log("Ending loop to VisualizeMap"); // DEBUG
    }

    RoomBase selectedRoomPrefab() {
        int randomValue = Random.Range(0, 100 + 1);

        RoomBase randomRoomSelected;

        // If the "randomValue <= 30" run the code
        if (randomValue <= 30) { // 30% chance to run and return "combatRoomPrefab"
            return randomRoomSelected = combatRoomPrefab;
        }
        // If the "randomValue <= 65" run the code
        else if (randomValue <= 65) { // 35% chance to run and return "treasureRoomPrefab"
            return randomRoomSelected = treasureRoomPrefab;
        }
        // If the "randomValue <= 100" run the code
        else  if (randomValue <= 100) { // 35% chance to run and return "puzzleRoomPrefab"
            return randomRoomSelected = puzzleRoomPrefab;
        }
        else {
            Debug.LogWarning("Error on the \"selectedRoomPrefab()\" at the script \"MapManager\"");
            return null;
        }
    }

}