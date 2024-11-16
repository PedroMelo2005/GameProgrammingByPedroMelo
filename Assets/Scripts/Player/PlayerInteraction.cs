using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour {
    [SerializeField] private Player _playerScript;
    private Interactable currentInteractable;

    public float playerReach = 4f;

    // Update is called once per frame
    void Update() {
        // Call function "CheckInteraction"
        CheckInteraction();

        // Check if player press keyboard key "E"
        if (Input.GetKeyDown(KeyCode.E) && currentInteractable != null) {
            currentInteractable.Interact();
        }
    }

    // Check if the player is interacting with an interactable object
    private void CheckInteraction() {
        RaycastHit hit;
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);

        // Check if colliders with anything within player reach
        if (Physics.Raycast(ray, out hit, playerReach)) {

            // Check if is looking at an interactable object
            if (hit.collider.tag == ConstVariables.InteractableTagName) {
                Interactable newInteractable = hit.collider.GetComponent<Interactable>();
                Debug.Log(hit.collider);

                if (currentInteractable && newInteractable != currentInteractable) {
                    DisableCurrentInteractable();
                }

                if (newInteractable.enabled) {
                    SetNewCurrentInteractable(newInteractable);
                    Debug.Log(hit.collider);
                }
                else {
                    DisableCurrentInteractable();
                }
            }
            else {
                DisableCurrentInteractable();
            }
        }
        else {
            DisableCurrentInteractable();
        }
    }

    private void SetNewCurrentInteractable(Interactable newInteractable) {
        currentInteractable = newInteractable;
        UIManager.instance.EnableInteractionText(currentInteractable.message);
    }

    private void DisableCurrentInteractable() {
        UIManager.instance.DisableInteractionText();

        if (currentInteractable) {
            currentInteractable = null;
        }
    }

}