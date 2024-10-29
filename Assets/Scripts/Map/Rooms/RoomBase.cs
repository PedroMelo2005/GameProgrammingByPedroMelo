using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomBase : MonoBehaviour {
    [SerializeField] private GameObject NorthDoorway, EastDoorway, SouthDoorway, WestDoorway;

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