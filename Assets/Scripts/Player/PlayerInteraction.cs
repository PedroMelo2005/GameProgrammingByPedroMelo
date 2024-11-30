using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour {
    [SerializeField] private Player _playerScript;
    [SerializeField] private GameObject raycastPoint;
    private Interactable currentInteractable;

    private float playerReach = 3f;

    // Update is called once per frame
    void Update() {
        // Call function "CheckInteractionText"
        CheckInteractionText();
        // Call function "CheckInteraction"
        CheckInteraction();

        /*
        if (Player.Instance.PlayerCanMove) {
            // Check if player press keyboard key "E"
            if (Input.GetKeyDown(KeyCode.E) && currentInteractable != null) {
                currentInteractable.Interact();
            }
        }
        */
    }

    private void CheckInteraction() {
        if (Player.Instance.PlayerCanMove) {
            if (Input.GetKeyDown(KeyCode.E)) {
                RaycastHit hit;
                Ray ray = new Ray(raycastPoint.transform.position, raycastPoint.transform.forward);

                // Check if colliders with anything within player reach
                if (Physics.Raycast(ray, out hit, playerReach)) {

                    // Check if is looking at an interactable object
                    if (hit.collider.GetComponent<Interactable>() != null) {
                        Interactable newInteractable = hit.collider.GetComponent<Interactable>();
                        Debug.Log(hit.collider); // DEBUG

                        if (hit.transform.CompareTag("Item")) {
                            //
                        }
                        else if (hit.collider.CompareTag("Lootable")) {
                            StartCoroutine(InventoryManager.Instance.CreatePanel(InventoryManager.Instance.GetPanel(InventoryPanel.Type.Loot), hit.transform.parent.GetComponent<LootData>()));
                            UIManager.Instance.SetActiveLootPanel(true);
                            UIManager.Instance.SetCanvasInventory(true);
                            currentInteractable.Interact();
                        }
                        else if (hit.collider.tag == "Interactable") {
                            currentInteractable.Interact();
                        }
                    }
                }
            }
        }
    }

    // Check if the player is interacting with an interactable object
    private void CheckInteractionText() {
        RaycastHit hit;
        Ray ray = new Ray(raycastPoint.transform.position, raycastPoint.transform.forward);

        // Check if colliders with anything within player reach
        if (Physics.Raycast(ray, out hit, playerReach)) {

            // Check if is looking at an interactable object
            if (hit.collider.GetComponent<Interactable>() != null) {
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

    private void CheckInteractable() {

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