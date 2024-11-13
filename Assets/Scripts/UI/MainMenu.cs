using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {
    // Make this gameObject be active in the scene
    public void SetMainMenu() {
        gameObject.SetActive(true);
        // Lock the Cursor to the game window and set "Cursor.visible" to true
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }

    public void ButtonStartGame() {
        // Assign the variable "IsMainMenuActive" to false
        UIManager.IsMainMenuActive = false;
        // Set the SetActive of the gameObject to false
        gameObject.SetActive(false);
        // Change the Scene of the game
        SceneManager.LoadScene("SampleScene");
    }

    public void QuitGame() {
        Application.Quit();
    }

}