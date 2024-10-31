using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    float speed = 7f;

    [SerializeField] CharacterController controller;

    // Start is called before the first frame update
    void Start() {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update() {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move =(transform.right * x + transform.forward * z).normalized;

        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.W)) {
            speed = 11f;
        }
        else if (Input.GetKey(KeyCode.LeftShift) && (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.S))) {
            speed = 9f;
        }
        else {
            speed = 6f;
        }

        controller.Move(move * speed * Time.deltaTime);
    }
}