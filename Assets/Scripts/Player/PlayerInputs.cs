using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerInputs : MonoBehaviour {
    [SerializeField] private Player _player;

    void Start () {

    }

    void Update() {
        // Call the function "CheckPauseGameMenu()"
        CheckPauseMenu();
        // Call the function "CheckInventoryMenu()"
        CheckInventoryMenu();
        // Call the function "CheckInventoryRotateItem()"
        CheckInventoryRotateItem();
    }

    // CheckPauseMenu
    private void CheckPauseMenu() {
        // Check if the player pressed the keyboard key "Escape(Esc)" and call the respective function
        if (Input.GetKeyDown(KeyCode.Escape)) {

            if (GameManager.Instance.IsGamePaused == true) {
                UIManager.Instance.HidePauseMenu();
            }
            else if (GameManager.Instance.IsGamePaused == false) {
                UIManager.Instance.ShowPauseMenu();
            }
            else {
                Debug.LogError("Error when CheckPauseMenu");
            }
        }
    }

    // CheckInventoryMenu
    private void CheckInventoryMenu() {
        if (UIManager.Instance.IsPauseMenuOpen == false) {

            // Check if the player pressed the keyboard key "Tab" and call the respective function
            if (Input.GetKeyDown(KeyCode.Tab)) {
                UIManager.Instance.TurnCanvasInventory();
                UIManager.Instance.SetActiveLootPanel(false);
            }
        }
    }

    /*
    // OldCheckInventoryMenu
    private void OldCheckInventoryMenu() {
        if (UIManager.Instance.IsPauseMenuOpen == false) {

            // Check if the player pressed the keyboard key "Tab" and call the respective function
            if (Input.GetKeyDown(KeyCode.Tab)) {

                if (UIManager.Instance.IsInventoryMenuOpen == true) {
                    UIManager.Instance.HideInventoryMenu();
                }
                else if (UIManager.Instance.IsInventoryMenuOpen == false) {
                    UIManager.Instance.ShowInventoryMenu();
                }
                else {
                    Debug.LogError("Error when CheckInventoryMenu");
                }
            }
        }
    }
    */

    // CheckInventoryRotateItem
    private void CheckInventoryRotateItem() {
        if (UIManager.Instance.IsInventoryMenuOpen == true) {
            if (Input.GetKeyDown(KeyCode.R)) {

            }
        }
    }

}