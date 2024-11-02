using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour {
    [SerializeField] private Player _playerScript;
    private RoomBase _currentRoom = null;

    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        
    }

    // Function is activate when the player enter the collision area
    private void OnTriggerEnter(Collider otherObject) {
        _currentRoom = otherObject.GetComponent<RoomBase>();
        _currentRoom.OnRoomEntered();
    }
    // Function is activate when the player exit the collision area
    private void OnTriggerExit(Collider otherObject) {
        _currentRoom = otherObject.GetComponent<RoomBase>();
        _currentRoom.OnRoomExit();
    }

}