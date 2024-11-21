using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    [SerializeField] private Player _playerScript;

    [SerializeField] private float moveSpeed = 4f;

    // Start is called before the first frame update
    void Start() {
        _playerScript.PhysicsBody = GetComponent<Rigidbody>();

        _playerScript.PhysicsBody.freezeRotation = true;
    }

    // Update is called once per frame
    void Update() {
        if (Player.Instance.PlayerCanMove) {
            // Get Inputs
            float moveX = Input.GetAxis("Horizontal"); // Input "A, D"
            float moveZ = Input.GetAxis("Vertical"); // Input "W, S"

            // Calculate the movement direction relative to the player's orientation
            Vector3 moveDirection = (transform.right * moveX + transform.forward * moveZ).normalized;

            if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.W)) {
                moveSpeed = 7.5f;
            }
            else if (Input.GetKey(KeyCode.LeftShift) && (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.S))) {
                moveSpeed = 5.5f;
            }
            else {
                moveSpeed = 4f;
            }

            // Apply velocity based movement to the Rigidbody
            Vector3 velocity = moveDirection * moveSpeed;

            // Preserve Y axis velocity for gravity
            _playerScript.PhysicsBody.velocity = new Vector3(velocity.x, _playerScript.PhysicsBody.velocity.y, velocity.z);
        }
    }

}