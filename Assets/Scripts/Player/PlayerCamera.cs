using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour {
    [SerializeField] private Player _playerScript;
    [SerializeField] private Transform playerHead;

    private float mouseSensibility = 145f;
    private float horizontalFacing = 0f;
    private float verticalFacing = 0f;

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        if (Player.PlayerCanMove) {


            // Get Inputs
            float mouseX = Input.GetAxis("Mouse X") * mouseSensibility * Time.deltaTime; // Input "Mouse Left, Right"
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensibility * Time.deltaTime; // Input "Mouse Up and Down"

            // Calculate the pitch (vertical rotation) and clamp it
            verticalFacing -= mouseY;
            verticalFacing = Mathf.Clamp(verticalFacing, -90f, 90f);
            horizontalFacing += mouseX;

            // Make the camera look up and down locally
            playerHead.localRotation = Quaternion.Euler(verticalFacing, 0f, 0f);
            // Apply the horizontal rotation to the entire player body
            _playerScript.PhysicsBody.rotation = Quaternion.Euler(0f, horizontalFacing, 0f);
        }
    }

}