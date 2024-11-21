using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour {
    [SerializeField] private Player _playerScript;
    [SerializeField] private GameObject RaycastPoint;
    private Interactable currentInteractable;

    public float playerReach = 3f;

    // Update is called once per frame
    void Update() {
        // Call function "CheckInteraction"
        CheckInteraction();

        if (Player.Instance.PlayerCanMove) {
            // Check if player press keyboard key "E"
            if (Input.GetKeyDown(KeyCode.E) && currentInteractable != null) {
                currentInteractable.Interact();
            }
        }
    }

    // Check if the player is interacting with an interactable object
    private void CheckInteraction() {
        RaycastHit hit;
        Ray ray = new Ray(RaycastPoint.transform.position, RaycastPoint.transform.forward);

        // Check if colliders with anything within player reach
        if (Physics.Raycast(ray, out hit, playerReach)) {

            // Check if is looking at an interactable object
            if (hit.collider.tag == ConstVariables.InteractableTagName) {
                Interactable newInteractable = hit.collider.GetComponent<Interactable>();
                Debug.Log(hit.collider); // DEBUG

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
        UIManager.Instance.EnableInteractionText(currentInteractable.message);
    }

    private void DisableCurrentInteractable() {
        UIManager.Instance.DisableInteractionText();

        if (currentInteractable) {
            currentInteractable = null;
        }
    }

}