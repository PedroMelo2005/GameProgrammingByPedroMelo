using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {
    [SerializeField] private UIManager UiSystem;
    [SerializeField] private InGameHud _inGameHud;

    // Start is called before the first frame update
    // Make this gameObject don't be active in the scene
    void Start() {
        gameObject.SetActive(false);
    }
    
    // Update is called once per frame
    void Update() {

    }

    // Set PauseGame actions
    public void PauseGame() {
        // Make this gameObject be active in the scene
        gameObject.SetActive(true);
        // Call function "DeactivateInGameHud()"
        _inGameHud.DeactivateInGameHud();
        // Freeze the time of the game
        Time.timeScale = 0f;
        // Assign the variable "IsPaused" to true
        UIManager.IsPaused = true;
        // Lock the Cursor to the game window and set "Cursor.visible" to true
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }

    // Set ContinueGame actions
    public void ContinueGame() {
        // Make this gameObject don't be active in the scene
        gameObject.SetActive(false);
        // Call function "ActivateInGameHud()"
        _inGameHud.ActivateInGameHud();
        // Unfreeze the time of the game
        Time.timeScale = 1f;
        // Assign the variable "IsPaused" to false
        UIManager.IsPaused = false;
        // Lock the Cursor to the center of the game window and set "Cursor.visible" to false
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void BackToMainMenu() {
        // Assign the variable "IsMainMenuActive" to true
        UIManager.IsMainMenuActive = true;
        // Change the Scene of the game
        SceneManager.LoadScene("MainMenu");
        // Unfreeze the time of the game
        Time.timeScale = 1f;
        // Assign the variable "IsPaused" to false
        UIManager.IsPaused = false;
        // Lock the Cursor to the game window and set "Cursor.visible" to true
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }

}