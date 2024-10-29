using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour {
    const int mapSize = 3;
    const int roomSize = 6;

    [SerializeField] GameObject welcomeRoomPrefab;
    [SerializeField] GameObject combatRoomPrefab;
    [SerializeField] GameObject treasureRoomPrefab;
    [SerializeField] GameObject puzzleRoomPrefab;

    // Create a List "roomList" of type GameObject
    List<GameObject> roomList;

    void Start() {
        // Initialize the "roomList"
        roomList = new List<GameObject> { combatRoomPrefab, treasureRoomPrefab, puzzleRoomPrefab };
        Debug.Log("Initialize the \"roomList\""); // DEBUG

        // Call function CreateMap
        CreateMap();
        Debug.Log("Call CreateMap"); // DEBUG
        // Call function VisualizeMap
        OldVisualizeMap();
        Debug.Log("Call OldVisualizeMap"); // DEBUG
    }

    public void CreateMap() {
        int midPoint = (mapSize - 1) / 2;
        for (int x = 0; x < mapSize; x++) {
            for (int z = 0; z < mapSize; z++) {
                if (x == midPoint && z == midPoint) {
                    GameObject startRoomInstance = Instantiate(welcomeRoomPrefab, transform);
                    startRoomInstance.transform.position = new Vector3(x, 0, z);
                }
                else {
                    GameObject randomRoomInstance = Instantiate(roomList[Random.Range(0, roomList.Count)], transform);
                    randomRoomInstance.transform.position = new Vector3(x, 0, z);
                }
            }
        }

    }

    public void OldVisualizeMap() {
        for (int x = 0; x < mapSize; x++) {
            for (int z = 0; z < mapSize; z++) {
                GameObject mapRoomRepresentation = GameObject.CreatePrimitive(PrimitiveType.Cube);
                mapRoomRepresentation.transform.position = new Vector3(x, 0, z);
            }
        }
    }
    public void SetupPlayer() {

    }

    // onn the map is ready set player position on map
    public void OnGameStart() {

    }

}