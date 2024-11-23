using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureRoom : RoomBase {
    [SerializeField] private LootData[] _containers;
    private LootData _chest;
    public LootData Chest => _chest;

    public static readonly List<LootData> ContainersList = new List<LootData>();

    int timesSearched = 0;
    public override string roomName { get; } = "Treasure Room";

    void Start() {
        InstantiateContainers();
    }

    public override void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            // Display message of which room the player enter
            Debug.Log($"You entered in the {roomName}"); // DEBUG
        }
    }

    public override void OnRoomSearched() {
        if (timesSearched < 3) {
            // Call function AddItemToInventory
            Inventory.Instance.AddItemToInventory(ref itemFound);
            Debug.Log($"You found: {itemFound}");
            timesSearched++;
        }
        else if (timesSearched >= 3) {
            Debug.Log($"{Player.Instance.PlayerName} you can't search more than 3 times on {roomName}");
        }
        else {
            Debug.Log("Nothing more on this room!");
        }
    }

    private void InstantiateContainers() {
        var newContainer = _containers[Random.Range(0, _containers.Length)];
        _chest = Instantiate(newContainer, transform);
        _chest.transform.localPosition = new Vector3(0, 1, 0);
        ContainersList.Add(_chest);
    }

}