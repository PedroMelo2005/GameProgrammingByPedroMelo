using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    // Create the Instances
    public static Player player; // Global player Instance
    private Player _player;
    private MapManager _gameMap;

    [SerializeField] public Player PlayerPreFab;
    [SerializeField] private MapManager MapManagerPreFab;

    // Start is called before the first frame update
    public void Start() {
        Debug.Log("Starting Game");
        InstantiateInstances();
        SetUpOnEverything();
        OnGameStart();
    }

    // Update is called once per frame
    void Update() {
        
    }

    void InstantiateInstances() {
        _player = Instantiate(PlayerPreFab, transform);
        _gameMap = Instantiate(MapManagerPreFab, transform);
    }
    
    void SetUpOnEverything() {
        // Call function CreateMap
        _gameMap.CreateMap();
        Debug.Log("Call CreateMap"); // DEBUG

        // Call function VisualizeMap
        _gameMap.OldVisualizeMap();
        Debug.Log("Call OldVisualizeMap"); // DEBUG
    }

    void OnGameStart() {

    }
}