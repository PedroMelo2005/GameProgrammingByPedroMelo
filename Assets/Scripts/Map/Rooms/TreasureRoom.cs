using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureRoom : RoomBase {
    int timesSearched = 0;
    public override string roomName { get; } = "Treasure Room";

    public override void OnRoomEntered() {
        // Call the base function OnRoomEntered
        base.OnRoomEntered();
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

}