using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder.MeshOperations;

public class MapManager : MonoBehaviour {
    const int mapSize = 3;
    const int roomSize = 6;

    [SerializeField] GameObject welcomeRoomPrefab;
    [SerializeField] GameObject combatRoomPrefab;
    [SerializeField] GameObject treasureRoomPrefab;
    [SerializeField] GameObject puzzleRoomPrefab;

    // Create a List "roomList" of type GameObject
    public List<GameObject> roomList;

    public void SetUpMapManager() {
        // Initialize the "roomList"
        Debug.Log("Initialize the \"roomList\""); // DEBUG
        roomList = new List<GameObject> { combatRoomPrefab, treasureRoomPrefab, puzzleRoomPrefab };
    }

    public void CreateMap() {
        // midPoint represents the center of the grid
        int midPoint = (mapSize - 1) / 2;

        Debug.Log("Starting loop to CreateMap"); // DEBUG
        // Run the code while the variable "x" is less than the "mapSize"
        for (int x = 0; x < mapSize; x++) {
            // Run the code while the variable "z" is less than the "mapSize"
            for (int z = 0; z < mapSize; z++) {

                // Create a variable "coords" that will calculate the coordinates X and Y of the room
                Vector2 coords = new Vector2(x * roomSize, z * roomSize);

                // If is in on the center of the grid will run this code
                if (x == midPoint && z == midPoint) {
                    GameObject startRoomInstance = Instantiate(welcomeRoomPrefab, transform);
                    startRoomInstance.transform.position = new Vector3(coords.x, 0, coords.y);
                }
                // Others rooms that are not on the center of the grid
                else {
                    GameObject randomRoomInstance = Instantiate(selectedRoomPrefab(), transform);
                    randomRoomInstance.transform.position = new Vector3(coords.x, 0, coords.y);
                }
            }
        }
        Debug.Log("Ending loop to CreateMap"); // DEBUG
    }

    public void VisualizeMap() {
        Debug.Log("Starting loop to VisualizeMap"); // DEBUG
        for (int x = 0; x < mapSize; x++) {
            for (int z = 0; z < mapSize; z++) {
                GameObject mapRoomRepresentation = GameObject.CreatePrimitive(PrimitiveType.Cube);
                mapRoomRepresentation.transform.position = new Vector3(x, 0, z);
            }
        }
        Debug.Log("Ending loop to VisualizeMap"); // DEBUG
    }

    public void SetupPlayer() {

    }

    GameObject selectedRoomPrefab() {
        int randomValue = Random.Range(0, 100 + 1);

        GameObject randomRoomSelected;

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