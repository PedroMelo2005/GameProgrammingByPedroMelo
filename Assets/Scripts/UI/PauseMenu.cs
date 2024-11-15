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
        GameManager.IsGamePaused = true;
        // Unlock the Cursor and set "Cursor.visible" to true
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    // Set ContinueGame actions
    public void ButtonContinueGame() {
        // Make this gameObject don't be active in the scene
        gameObject.SetActive(false);
        // Call function "ActivateInGameHud()"
        _inGameHud.ActivateInGameHud();
        // Unfreeze the time of the game
        Time.timeScale = 1f;
        // Assign the variable "IsPaused" to false
        GameManager.IsGamePaused = false;
        // Lock the Cursor to the center of the game window and set "Cursor.visible" to false
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void ButtonBackToMainMenu() {
        // Change the Scene of the game
        SceneManager.LoadScene(ConstVariables.MainMenuSceneName);
        // Unfreeze the time of the game
        Time.timeScale = 1f;
        // Assign the variable "IsPaused" to false
        GameManager.IsGamePaused = false;
        // Lock the Cursor to the game window and set "Cursor.visible" to true
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }

}